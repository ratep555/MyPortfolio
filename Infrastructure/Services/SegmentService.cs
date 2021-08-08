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
    public class SegmentService : ISegmentService
    {
        private readonly PortfolioContext _context;
        public SegmentService(PortfolioContext context)
        {
            _context = context;

        }

        public async Task<IQueryable<Segment>> GetSegmentsWithSearching(QueryParameters queryParameters)
        {
            IQueryable<Segment> segment = _context.Segments.AsQueryable()
                                        .OrderBy(x => x.Label);
            
            if (queryParameters.HasQuery())
            {
                segment = segment
                .Where(t => t.Label.Contains(queryParameters.Query));
            }

            return await Task.FromResult(segment);     
        }
        
        public async Task<IQueryable<Segment>> GetSegmentsWithPaging(QueryParameters queryParameters)
        {
            var segment = await GetSegmentsWithSearching(queryParameters);

            segment = segment.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await Task.FromResult(segment);     
        }

        public async Task<Segment> GetSegmentByIdAsync(int id)
        {
            return await _context.Segments         
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateSegment(Segment segment)
        {
             _context.Segments.Add(segment);
             await _context.SaveChangesAsync();                    
        }

        public async Task UpdateSegment(Segment segment)
        {
             _context.Entry(segment).State = EntityState.Modified;        
             await _context.SaveChangesAsync();
        }

        public async Task DeleteSegment(Segment segment)
        {
                _context.Segments.Remove(segment);
                await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Segments.CountAsync();
        }
    }
}








