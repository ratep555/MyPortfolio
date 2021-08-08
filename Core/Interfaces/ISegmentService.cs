using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISegmentService
    {
        Task<IQueryable<Segment>> GetSegmentsWithSearching(QueryParameters queryParameters);
        Task<IQueryable<Segment>> GetSegmentsWithPaging(QueryParameters queryParameters);
        Task<Segment> GetSegmentByIdAsync(int id);   
        Task CreateSegment(Segment segment);     
        Task UpdateSegment(Segment segment);
       Task DeleteSegment(Segment segment);
      

    }
}