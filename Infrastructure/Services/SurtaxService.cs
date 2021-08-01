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
    public class SurtaxService : ISurtaxService
    {
        private readonly PortfolioContext _context;
        public SurtaxService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Surtax>> GetSurtaxesWithSearching(QueryParameters queryParameters)
        {
            IQueryable<Surtax> surtax = _context.Surtaxes.AsQueryable()
                                        .OrderBy(x => x.Residence);
            
            if (queryParameters.HasQuery())
            {
                surtax = surtax
                .Where(t => t.Residence.Contains(queryParameters.Query));            
            }
            
            return await Task.FromResult(surtax);        
        }

        public async Task<IQueryable<Surtax>> GetSurtaxesWithPaging(QueryParameters queryParameters)
        {
            var surtax = await GetSurtaxesWithSearching(queryParameters);

            surtax = surtax.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await Task.FromResult(surtax);     
        }


        public async Task<Surtax> GetSurtaxByIdAsync(int id)
        {
            return await _context.Surtaxes         
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateSurtax(Surtax surtax)
        {
            _context.Surtaxes.Add(surtax);
            await _context.SaveChangesAsync();                    
        }

        public async Task UpdateSurtax(Surtax surtax)
        {
            _context.Entry(surtax).State = EntityState.Modified;        
             await _context.SaveChangesAsync();
        }

        public async Task DeleteSurtax(Surtax surtax)
        {
            _context.Surtaxes.Remove(surtax);
            await _context.SaveChangesAsync();
        }
    }
}







