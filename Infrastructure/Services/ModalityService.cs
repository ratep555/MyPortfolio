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
    public class ModalityService : IModalityService
    {
        private readonly PortfolioContext _context;
        public ModalityService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<List<Modality>> GetModalitiesWithSearchingAndPaging(QueryParameters queryParameters)
        {
            IQueryable<Modality> modality = _context.Modalities.AsQueryable()
                                        .OrderBy(x => x.Label);
            
            if (queryParameters.HasQuery())
            {
                modality = modality
                .Where(t => t.Label.Contains(queryParameters.Query));
            }

            modality = modality.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await modality.ToListAsync();   
        }

        public async Task<int> GetCountForModalities()
        {
            return await _context.Modalities.CountAsync();
        }
     
        public async Task<Modality> GetModalityByIdAsync(int id)
        {
            return await _context.Modalities         
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateModality(Modality modality)
        {
             _context.Modalities.Add(modality);
             await _context.SaveChangesAsync();                    
        }

        public async Task UpdateModality(Modality modality)
        {
             _context.Entry(modality).State = EntityState.Modified;        
             await _context.SaveChangesAsync();
        }

        public async Task DeleteModality(Modality modality)
        {
                _context.Modalities.Remove(modality);
                await _context.SaveChangesAsync();
        }
    }
}











