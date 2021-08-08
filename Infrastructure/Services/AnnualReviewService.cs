using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AnnualReviewService : IAnnualReviewService
    {
        private readonly PortfolioContext _context;
        public AnnualReviewService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task ActionsRegardingProfitOrLossCardUponLogin(string email)
        {
            var card = await FindCardByEmail(email);

             if (_context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false).Any())
            {
                if (DateTime.Now.Year > card.Year)
                {
                    await LockExistingCard(card);

                    await CreateNewCard(email);
                }                
            }
        }

        public async Task ActionsRegardingProfitOrLossCardUponPurchase(string email)
        {
            if (!_context.StockTransactions.Where(x => x.Email == email).Any())
            {
                await CreateNewCard(email);                           
            }
            else
            {
                await LockExistingCardAndCreateNew(email);
            }
        }

        public async Task ActionsRegardingProfitOrLossCardUponSelling(string email)
        {
            var card = await FindCardByEmail(email);

            if (DateTime.Now.Year == card.Year)
            {
                await UpdateCard(card, email);              
            }

            if (DateTime.Now.Year > card.Year)
            {
                await LockExistingCard(card);

                await CreateNewCard(email);
            }          
        }     

        public async Task TwoYearException(string email, StockTransaction transaction)
        {
            var card = await FindCardByEmail(email);

            decimal basket = 0;
            var today = transaction.Date;

            var transactionOlderThenTwoyears = await _context.StockTransactions
                                               .Where(x => x.Email == transaction.Email && x.StockId == transaction.StockId 
                                               && x.Purchase == true && x.Resolved != x.Quantity).FirstOrDefaultAsync();

            foreach (var item in _context.Stocks.Where(x => x.Id == transaction.StockId).ToList())
            {
                  if (transactionOlderThenTwoyears.Date < transaction.Date.AddYears(-2))
                  {
                    decimal sumOfLastSelling = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                                .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity * x.Price).Sum();

                    int quantityOfLastSelling = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                            .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity).Sum();

                    var list1 = _context.StockTransactions.Where(x => x.Purchase == true && x.Resolved > 0 && x.StockId == item.Id);

                    decimal sumOfFirstPurchase = SumOfLastTransactions(list1, quantityOfLastSelling);

                    basket += (sumOfLastSelling - sumOfFirstPurchase);  
                 }
            }  

            card.TaxExemption += basket;

             _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task<AnnualProfitOrLoss> FindCardByEmail(string email)
        {
            return await _context.AnnualProfitsOrLosses
                         .Where(a => a.Email == email && a.Locked == false).FirstOrDefaultAsync();
        }

        private async Task CreateNewCard(string email)
        {
            var card = new AnnualProfitOrLoss
            {
                Year = DateTime.Now.Year,
                TaxableIncome = 0,
                TaxExemption = 0,
                Amount = 0,
                Email = email,
                Locked = false
            };

            _context.AnnualProfitsOrLosses.Add(card);
            await _context.SaveChangesAsync();       
        }

        private async Task UpdateCard(AnnualProfitOrLoss card, string email)
        {
            card.Year = DateTime.Now.Year;
            card.Email = email;
            card.Amount = await TotalNetProfitOrLossForCurrentYear(email);

            if (card.Amount > 0)
            {
                card.TaxableIncome = card.Amount - card.TaxExemption;             
            }
            else
            {
                card.TaxableIncome = 0;
            }
 
            card.Locked = false;
        
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task LockExistingCard(AnnualProfitOrLoss card)
        {
            card.Locked = true;
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task LockExistingCardAndCreateNew(string email)
        {
            var card = await FindCardByEmail(email);
      
            if (_context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false).Any())                          
            {
                if (DateTime.Now.Year > card.Year)
                {
                    await LockExistingCard(card);

                    await CreateNewCard(email);
                }
            }
        }      

        private async Task<decimal?> TotalNetProfitOrLossForCurrentYear(string email)
        { 
            var card = await FindCardByEmail(email);
            
            int currentYear = card.Year;
            decimal? basket = 0;
            decimal? totalNetProfitOrLoss = 0;
            decimal valueOfPurchase = 0;
            decimal valueOfSelling = 0;          
            int num = 0;
           
            foreach (var item in _context.Stocks.ToList())
            {                               
                var listOfLeftovers = await _context.StockTransactions.Where(x => x.Email == email 
                                            && x.StockId == item.Id && x.Resolved != 0 && x.Purchase == true)
                                            .OrderByDescending(x => x.Id).ToListAsync();

                num = _context.StockTransactions
                      .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false 
                      && t.Date.Year == currentYear)
                      .Sum(t => t.Quantity);               
                
                valueOfPurchase = SumOfLastTransactions(listOfLeftovers, num);

                valueOfSelling = _context.StockTransactions
                                 .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false 
                                 && t.Date.Year == currentYear)
                                 .Sum(t => t.Price * t.Quantity);

                totalNetProfitOrLoss = valueOfSelling - valueOfPurchase;

                basket += totalNetProfitOrLoss;                         
            }

            return await Task.FromResult(basket);
        }

        private decimal SumOfFirstTransactions(IEnumerable<StockTransaction> stockTransactions, int max)
        {
            decimal result = 0;
            int sum = 0;
            foreach(var stockTransaction in stockTransactions.OrderBy(x => x.Id))
            {           
                if(sum + stockTransaction.Quantity <= max)
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
     
        private decimal SumOfLastTransactions(IEnumerable<StockTransaction> stockTransactions, int max)
        {
            decimal result = 0;
            int sum = 0;
            foreach(var stockTransaction in stockTransactions.OrderByDescending(x => x.Id))
            {           
                if(sum + stockTransaction.Quantity <= max)
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
    }
}










