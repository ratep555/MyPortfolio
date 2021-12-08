using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISegmentService
    {
        Task<List<Segment>> GetSegmentsWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForSegments();
        Task<Segment> GetSegmentByIdAsync(int id);   
        Task CreateSegment(Segment segment);     
        Task UpdateSegment(Segment segment);
       Task DeleteSegment(Segment segment);
      

    }
}