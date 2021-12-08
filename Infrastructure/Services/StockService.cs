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
    public class StockService : IStockService
    {
        private readonly PortfolioContext _context;
        public StockService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetStocksWithSearchingAndPaging(QueryParameters queryParameters)
        {
            IQueryable<Stock> stocks = _context.Stocks.Include(x => x.Category).Include(x => x.Modality)
                .Include(x => x.Segment).Include(x => x.Type)
                .AsQueryable().OrderBy(x => x.Symbol);
            
            if (queryParameters.HasQuery())
            {
                stocks = stocks
                    .Where(x => x.Symbol.Contains(queryParameters.Query));
            }

            if (queryParameters.CategoryId.HasValue)
            {
                stocks = stocks.Where(x => x.CategoryId == queryParameters.CategoryId);
            }

            stocks = stocks.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
            
            return await stocks.ToListAsync();
        }
        
        public async Task<int> GetCountForStocks()
        {
            return await _context.Stocks.CountAsync();
        }

        public async Task<Stock> GetStockByIdAsync(int id)
        {
            return await _context.Stocks
            .Include(x => x.Category)
            .Include(x => x.Modality)
            .Include(x => x.Segment)
            .Include(x => x.Type)            
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateStock(Stock stock)
        {
             _context.Stocks.Add(stock);
             await _context.SaveChangesAsync();                    
        }

        public async Task UpdateStock(Stock stock)
        {
             _context.Entry(stock).State = EntityState.Modified;        
             await _context.SaveChangesAsync();
        }

        public async Task DeleteStock(Stock stock)
        {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.OrderBy(x => x.CategoryName).ToListAsync();
        }

        public async Task<List<Modality>> GetModalities()
        {
            return await _context.Modalities.OrderBy(x => x.Label).ToListAsync();
        }

        public async Task<List<Segment>> GetSegments()
        {
            return await _context.Segments.OrderBy(x => x.Label).ToListAsync();
        }

        public async Task<List<TypeOfStock>> GetTypesOfStock()
        {
            return await _context.TypesOfStock.OrderBy(x => x.Label).ToListAsync();
        }

        public async Task RefreshPrices(string symbol, decimal price)
        {
            var list = await _context.Stocks.Where(x => x.Symbol == symbol)
                       .Include(x => x.Category)
                       .Include(x => x.Modality)
                       .Include(x => x.Segment)
                       .Include(x => x.Type)
                       .ToListAsync();            
               
            foreach (var item in list)
            {
                item.Symbol = symbol;
                item.CurrentPrice = price;
                          
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();      
            }                          
        }
    
    }
}










