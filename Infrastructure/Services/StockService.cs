using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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

        public async Task<Stock> GetStockByIdAsync(int id)
        {
            return await _context.Stocks
            .Include(p => p.Category)
            .Include(p => p.Modality)
            .Include(p => p.Segment)
            .Include(p => p.Type)            
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync()
        {
            return await _context.Stocks
            .Include(p => p.Category)
            .Include(p => p.Modality)
            .Include(p => p.Segment)
            .Include(p => p.Type)
            .ToListAsync();
        }

    }
}