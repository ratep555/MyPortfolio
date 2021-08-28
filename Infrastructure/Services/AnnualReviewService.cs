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
    public class AnnualReviewService : IAnnualReviewService
    {
        private readonly PortfolioContext _context;
        public AnnualReviewService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<AnnualProfitOrLoss>> GetAnnualProfitOrLossWithSearching(
            QueryParameters queryParameters, string email)
        {
            IQueryable<AnnualProfitOrLoss> annualProfitOrLoss = _context.AnnualProfitsOrLosses
                                                                .Where(x => x.Email == email).AsQueryable()
                                                                .OrderBy(x => x.Year);
            
            if (queryParameters.HasQuery())
            {
                annualProfitOrLoss = annualProfitOrLoss
                .Where(t => t.Year.ToString().Contains(queryParameters.Query));            
            }
            
            return await Task.FromResult(annualProfitOrLoss);        
        }

        public async Task<IQueryable<AnnualProfitOrLoss>> GetAnnualProfitOrLossWithPaging(
            QueryParameters queryParameters, string email)
        {
            var annualProfitOrLoss = await GetAnnualProfitOrLossWithSearching(queryParameters, email);

            annualProfitOrLoss = annualProfitOrLoss.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                                 .Take(queryParameters.PageCount);
            
            return await Task.FromResult(annualProfitOrLoss);     
        }

        public async Task ActionsRegardingProfitOrLossCardUponLogin(string email)
        {
            var card = await FindUnlockedCardByEmail(email);

            if (await _context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false).AnyAsync())
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
            var transactions = await _context.StockTransactions.Where(x => x.Email == email).AnyAsync();
            
            if (!transactions)
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
            var card = await FindUnlockedCardByEmail(email);

            if (DateTime.Now.Year == card.Year)
            {
                await UpdateCard(card, email);              
            }

            if (DateTime.Now.Year > card.Year)
            {
                await LockExistingCard(card);

                await CreateNewCardAndUpdateIt(email);                
            }          
        }     

        public async Task TwoYearException(string email, StockTransaction transaction)
        {
            var card = await FindUnlockedCardByEmail(email);

            decimal basket = 0;
            var today = transaction.Date;

            var transactionOlderThenTwoyears = await _context.StockTransactions
                                               .Where(x => x.Email == transaction.Email && x.StockId == transaction.StockId 
                                               && x.Purchase == true && x.Resolved != x.Quantity).FirstOrDefaultAsync();

            foreach (var item in _context.Stocks.Where(x => x.Id == transaction.StockId).ToList())
            {
                if (transactionOlderThenTwoyears.Date < transaction.Date.AddYears(-2))
                {
                    decimal sumOfLastSelling = transaction.Quantity * transaction.Price;

                    int quantityOfLastSelling = transaction.Quantity;

                    var list1 = _context.StockTransactions.Where(x => x.Purchase == true  && x.Resolved != x.Quantity  && x.StockId == item.Id);

                    decimal sumOfFirstPurchase = SumOfLastTransactions(list1, quantityOfLastSelling);

                    basket += (sumOfLastSelling - sumOfFirstPurchase);  
                }
            }  

            card.TaxExemption += basket;

            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<AnnualTaxLiabilityDto> ShowTaxLiability(string email, int id)
        {
            var card = await FindUnlockedCardByEmail(email);

            var surtax = await FindSurtaxById(id);

            var final = new AnnualTaxLiabilityDto();

            decimal f = 100;
            decimal? basket = card.TaxableIncome;;
            decimal? basket1 = (12 / f) * basket;

            final.Email = email;
            final.Amount = card.Amount;
            final.TaxableIncome = card.TaxableIncome;
            final.Year = card.Year;
            final.Residence = surtax.Residence;
            final.SurtaxPercentage = surtax.Amount;

            if (basket.HasValue && basket > 0)
            {
                final.CapitalGainsTax = basket1;
            }
             else
            {
                final.CapitalGainsTax = 0;
            }

            if (basket.HasValue && basket > 0)
            {
                final.SurtaxAmount = (surtax.Amount / f) * basket1;
            }
            else
            {
                final.SurtaxAmount = 0;
            }

            if (basket.HasValue && basket > 0)
            {
                final.TotalTaxLiaility = basket1 + final.SurtaxAmount;
            }
            else
            {
                final.TotalTaxLiaility = 0;
            }

            if (basket.HasValue && basket > 0)
            {
                final.NetProfit = basket - final.TotalTaxLiaility;
            }
            else
            {
                final.NetProfit = 0;
            }

            return final;
        }
     
        public async Task<AnnualTaxLiabilityDto> ShowAnnual(string email)
        {
            var card = await FindUnlockedCardByEmail(email);

            if (card != null)
            {
                var final = new AnnualTaxLiabilityDto();

                final.Email = email;
                final.Amount = card.Amount;
                final.TaxableIncome = card.TaxableIncome;
                final.Year = card.Year;
          
                return final;
            }

            return new AnnualTaxLiabilityDto() 
            {Email = email, Year = DateTime.Now.Year, Amount = 0, TaxableIncome = 0};           
        }

        public async Task<IEnumerable<AnnualProfitOrLoss>> ShowListOfAnnualProfitAndLoss(string email)
        {
            var list = await _context.AnnualProfitsOrLosses.Where(x => x.Email == email).ToListAsync();

            return list;
        } 
     
        private async Task<AnnualProfitOrLoss> FindUnlockedCardByEmail(string email)
        {
            return await _context.AnnualProfitsOrLosses
                         .Where(x => x.Email == email && x.Locked == false)
                         .FirstOrDefaultAsync();
        }
        private async Task<Surtax> FindSurtaxById(int id)
        {
            return await _context.Surtaxes.Where(x => x.Id == id)
                         .FirstOrDefaultAsync();
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

        private async Task CreateNewCardAndUpdateIt(string email)
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

            card.Amount = await CalculateAmount(email);  

            if (card.Amount > 0)
            {
                card.TaxableIncome = card.Amount - card.TaxExemption;             
            }
            else
            {
                card.TaxableIncome = 0;
            }

            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        private async Task UpdateCard(AnnualProfitOrLoss card, string email)
        {
            card.Year = DateTime.Now.Year;
            card.Email = email;
            card.Amount = await CalculateAmount(email);

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
            var card = await FindUnlockedCardByEmail(email);
      
            if (await _context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false).AnyAsync())                          
            {
                if (DateTime.Now.Year > card.Year)
                {
                    await LockExistingCard(card);

                    await CreateNewCard(email);
                }
            }
        }      

        private async Task<decimal?> TotalNetProfitOrLoss(string email)
        { 
            var card = await FindUnlockedCardByEmail(email);
            
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

        private async Task<decimal?> CalculateAmount(string email)
        {            
            var card = await FindUnlockedCardByEmail(email);

            var list = await _context.AnnualProfitsOrLosses
                             .Where(x => x.Email == email && x.Locked == true).ToListAsync();
            
            decimal? basket = 0;
            decimal? basket1 = await TotalNetProfitOrLoss(email);

            if (await _context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == true).AnyAsync())
            {
                foreach (var item in list)
                {                  
                    basket += item.Amount;                   
                }
            }
            return basket1 - basket;
        }
    }
}










