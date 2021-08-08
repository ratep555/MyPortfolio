using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IModalityService
    {
        Task<IQueryable<Modality>> GetModalitiesWithSearching(QueryParameters queryParameters);
        Task<IQueryable<Modality>> GetModalitiesWithPaging(QueryParameters queryParameters);
        Task<Modality> GetModalityByIdAsync(int id);
        Task CreateModality(Modality modality);
        Task UpdateModality(Modality modality);
        Task DeleteModality(Modality modality);
       

    }
}