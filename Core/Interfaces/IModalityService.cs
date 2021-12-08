using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IModalityService
    {
        Task<List<Modality>> GetModalitiesWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForModalities();
        Task<Modality> GetModalityByIdAsync(int id);
        Task CreateModality(Modality modality);
        Task UpdateModality(Modality modality);
        Task DeleteModality(Modality modality);
       

    }
}