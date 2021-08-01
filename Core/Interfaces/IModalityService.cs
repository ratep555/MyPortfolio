using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IModalityService
    {
        Task<IQueryable<Modality>> GetModalitiesAsync(QueryParameters queryParameters);
        Task<Modality> GetModalityByIdAsync(int id);
        Task CreateModality(Modality modality);
        Task UpdateModality(Modality modality);
        Task DeleteModality(Modality modality);
       

    }
}