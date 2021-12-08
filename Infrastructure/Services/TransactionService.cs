using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly PortfolioContext _context;
        public TransactionService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<StockTransaction> BuyStockAsync(TransactionDto transactionDto, int id, string email)
        {
            var transaction = new StockTransaction
            {
                Date = transactionDto.DateOfTransaction.ToLocalTime(),
                StockId = id,
                Purchase = true,
                Quantity = transactionDto.Quantity,
                Price = transactionDto.Price,
                Resolved = 0,
                Email = email
            };

            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<StockTransaction> SellStockAsync(TransactionDto transactionDto, int id, string email)
        {
            var transaction = new StockTransaction
            {
                Date = transactionDto.DateOfTransaction.ToLocalTime(),
                StockId = id,
                Purchase = false,
                Quantity = transactionDto.Quantity,
                Price = transactionDto.Price,
                Resolved = 0,
                Email = email
            };

            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task UpdateResolved(int stockId, string email)
        {
            int soldQuantity = 0;

            var list = await _context.StockTransactions
            .Where(x => x.StockId == stockId && x.Email == email).ToListAsync();

            foreach (var item in list)
            {
                if (item.Purchase == false)
                {
                    soldQuantity = soldQuantity + item.Quantity;
                }
            }

            foreach (var item in list)
            {
                if (item.Purchase == true)
                {
                    var model1 = await _context.StockTransactions.Where
                    (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefaultAsync();

                    if (model1 != null)
                    {
                        if (soldQuantity > 0)
                        {
                            var newSoldQuantity = soldQuantity - item.Quantity;

                            if (newSoldQuantity >= 0)
                            {
                                model1.Resolved = item.Quantity;

                                await _context.SaveChangesAsync();
                            }
                            else if (newSoldQuantity < 0)
                            {
                                model1.Resolved = soldQuantity;

                                await _context.SaveChangesAsync();
                            }
                            soldQuantity = newSoldQuantity;
                        }
                    }
                }
            }
        }

        public async Task<int> TotalQuantity(string email, int stockId)
        {
            int totalQuantity = (_context.StockTransactions
            .Where(t => t.Email == email && t.StockId == stockId && t.Purchase == true)
            .Sum(t => t.Quantity)) -
            (_context.StockTransactions
            .Where(t => t.Email == email && t.StockId == stockId && t.Purchase == false)
            .Sum(t => t.Quantity));

            return await Task.FromResult(totalQuantity);
        }

        public async Task<IEnumerable<ClientPortfolioDto>> ShowClientPortfolioDto(string email)
        {
            var clientPortfolio = await (from t in _context.StockTransactions
                                         where t.Email == email
                                         join s in _context.Stocks
                                         on t.StockId equals s.Id
                                         join u in _context.Users.Where(u => u.Email == email)
                                         on t.Email equals u.Email
                                         select new ClientPortfolioDto
                                         {
                                             StockId = s.Id,
                                             TransactionId = t.Id,
                                             Symbol = s.Symbol,
                                             CurrentPrice = s.CurrentPrice,
                                             Email = email,
                                             TotalQuantity = (_context.StockTransactions.
                                             Where(b => b.StockId == s.Id && b.Email == u.Email && b.Purchase == true).
                                             Sum(b => (int?)b.Quantity) ?? 0) - (_context.StockTransactions.
                                             Where(b => b.StockId == s.Id && b.Email == u.Email && b.Purchase == false).
                                             Sum(b => (int?)b.Quantity) ?? 0),
                                         }).ToListAsync();

            foreach (var item in clientPortfolio)
            {
                var num = item.TotalQuantity;

                if (item.TotalQuantity > 0)
                {
                    var reverselist = _context.StockTransactions
                        .Where(x => x.StockId == item.StockId && x.Email == email & x.Purchase == true)
                        .OrderByDescending(x => x.Id);

                    var left = SumOfLastTransactions(reverselist, num);
                    item.AveragePriceOfPurchase = left / num;
                    item.TotalPriceOfPurchasePerStock = item.AveragePriceOfPurchase * item.TotalQuantity;
                    item.TotalMarketValuePerStock = item.CurrentPrice * item.TotalQuantity;
                }
            }

            return clientPortfolio.Where(d => d.TotalQuantity > 0).OrderBy(d => d.Symbol)
                .GroupBy(d => d.Symbol).Select(d => d.FirstOrDefault());
        }

        public async Task<IEnumerable<ClientPortfolioDto>> ShowClientPortfolio(
            QueryParameters queryParameters, string email)
        {
            decimal basket4 = 0;
            decimal basket6 = 0;
            decimal? basket7 = 0;

            var list = await ShowClientPortfolioDto(email);

            foreach (var item in list)
            {
                basket4 = item.AveragePriceOfPurchase * item.TotalQuantity;
                basket6 = list.Where(x => x.TotalQuantity > 0).Sum(x => x.TotalQuantity * x.AveragePriceOfPurchase);

                if (basket7.HasValue)
                {
                    basket7 = (basket4 / basket6) * 100;
                    item.PortfolioPercentage = basket7;
                }
                else
                {
                    item.PortfolioPercentage = 0;
                }
            }

            if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.Symbol.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return list;
        }

        public async Task<ClientPortfolioWithProfitOrLossDto> ClientPortfolioWithProfitOrLoss(
            QueryParameters queryParameters, string email)
        {
            var list = await ShowClientPortfolio(queryParameters, email);

            decimal basket = 0;
            decimal basket1 = 0;

            foreach (var item in list)
            {
                basket += item.TotalPriceOfPurchasePerStock;
                basket1 += item.TotalMarketValuePerStock;
            }

            var currentProfitOrLoss = new ClientPortfolioWithProfitOrLossDto();
            currentProfitOrLoss.ClientPortfolios = list;
            currentProfitOrLoss.TotalPriceOfPurchase = basket;
            currentProfitOrLoss.TotalMarketValue = basket1;

            return currentProfitOrLoss;
        }

        public async Task<TransactionsForUserWithProfitAndTrafficDto> ShowTransactionsWithProfitAndTraffic(
            QueryParameters queryParameters, string email)
        {
            TransactionsForUserWithProfitAndTrafficDto list = new TransactionsForUserWithProfitAndTrafficDto();

            IQueryable<TransactionForUserDto> transactions =
                              (from t in _context.StockTransactions
                               join u in _context.Users.Where(u => u.Email == email)
                               on t.Email equals u.Email
                               join s in _context.Stocks
                               on t.StockId equals s.Id
                               select new TransactionForUserDto
                               {
                                   Id = t.Id,
                                   StockId = s.Id,
                                   Stock = s.Symbol,
                                   Quantity = t.Quantity,
                                   Purchase = t.Purchase,
                                   Price = t.Price,
                                   Resolved = t.Resolved,
                                   Date = t.Date,
                                   Email = email,
                                   NetProfit = t.Quantity * t.Price
                               }).AsQueryable().OrderBy(s => s.Date);

            if (queryParameters.HasQuery())
            {
                transactions = transactions
                .Where(t => t.Stock.Contains(queryParameters.Query));
            }

            list.ListOfTransactions = await transactions.ToListAsync();

            decimal? basket1 = 0;
            decimal? basket2 = 0;
            decimal? basket3 = 0;

            foreach (var item in transactions)
            {
                basket1 += item.NetProfit;
            }

            foreach (var item in transactions)
            {
                if (item.Purchase == false)
                {
                    basket2 += item.NetProfit;
                }
            }

            foreach (var item in transactions)
            {
                if (item.Purchase == true && item.Resolved > 0)
                {
                    basket3 += (item.Price * item.Resolved);
                }
            }

            list.TotalNetProfit = basket2 - basket3;
            list.TotalTraffic = basket1;

            return list;
        }

        private decimal SumOfLastTransactions(IEnumerable<StockTransaction> stockTransactions, int max)
        {
            decimal result = 0;
            int sum = 0;
            foreach (var stockTransaction in stockTransactions.OrderByDescending(x => x.Id))
            {
                if (sum + stockTransaction.Quantity <= max)
                {
                    result += stockTransaction.Quantity * stockTransaction.Price;
                    sum += stockTransaction.Quantity;
                }
                else
                {
                    result += (max - sum) * stockTransaction.Price;
                    return result;
                }
            }
            return result;
        }

        public async Task<IEnumerable<ClientPortfolioChartDto>> GetChartForClientPortfolio(string email)
        {
            List<ClientPortfolioChartDto> chartlist = new List<ClientPortfolioChartDto>();

            var list = await ShowClientPortfolioDto(email);

            decimal basket4 = 0;
            decimal basket6 = 0;
            decimal? basket7 = 0;

            foreach (var item in list)
            {
                basket4 = item.AveragePriceOfPurchase * item.TotalQuantity;
                basket6 = list.Where(x => x.TotalQuantity > 0).Sum(x => x.TotalQuantity * x.AveragePriceOfPurchase);

                if (basket7.HasValue)
                {
                    basket7 = (basket4 / basket6) * 100;
                    item.PortfolioPercentage = basket7;
                }
                else
                {
                    item.PortfolioPercentage = 0;
                }

                chartlist.Add(new ClientPortfolioChartDto { Symbol = item.Symbol, 
                        CountForSymbol = (int)item.PortfolioPercentage  });
            }

            return chartlist;
        }
        public async Task<int> GetCountForChart(string email)
        {
            var list = await GetChartForClientPortfolio(email);

            return list.Count();
        }
    }
}










