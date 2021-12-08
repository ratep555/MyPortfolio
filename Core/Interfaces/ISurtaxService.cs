using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISurtaxService
    {
        Task<List<Surtax>> GetSurtaxesWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForSurtaxes();
        Task<IEnumerable<Surtax>> ListAllSurtaxes();
        Task<Surtax> GetSurtaxByIdAsync(int id);
        Task CreateSurtax(Surtax surtax);
        Task UpdateSurtax(Surtax surtax);
        Task DeleteSurtax(Surtax surtax);
    }
}