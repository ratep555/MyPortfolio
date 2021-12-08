using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TypeOfStockService : ITypeOfStockService
    {
        private readonly PortfolioContext _context;
        public TypeOfStockService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<List<TypeOfStock>> GetTypesOfStockWithSearchingAndPaging(QueryParameters queryParameters)
        {
            IQueryable<TypeOfStock> typeOfStock = _context.TypesOfStock.AsQueryable()
                                        .OrderBy(x => x.Label);
            
            if (queryParameters.HasQuery())
            {
                typeOfStock = typeOfStock
                .Where(t => t.Label.Contains(queryParameters.Query));
            }

           typeOfStock = typeOfStock.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await typeOfStock.ToListAsync();     
        }

        public async Task<int> GetCountForTypesOfStock()
        {
            return await _context.TypesOfStock.CountAsync();
        }
       
        public async Task<TypeOfStock> GetTypeOfStockByIdAsync(int id)
        {
            return await _context.TypesOfStock         
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateTypeOfStock(TypeOfStock typeOfStock)
        {
             _context.TypesOfStock.Add(typeOfStock);
             await _context.SaveChangesAsync();                    
        }

        public async Task UpdateTypeOfStock(TypeOfStock typeOfStock)
        {
             _context.Entry(typeOfStock).State = EntityState.Modified;        
             await _context.SaveChangesAsync();
        }

        public async Task DeleteTypeOfStock(TypeOfStock typeOfStock)
        {
                _context.TypesOfStock.Remove(typeOfStock);
                await _context.SaveChangesAsync();
        }
    }
}










