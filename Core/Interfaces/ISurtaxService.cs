using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISurtaxService
    {
        Task<IQueryable<Surtax>> GetSurtaxesWithSearching(QueryParameters queryParameters);
        Task<IQueryable<Surtax>> GetSurtaxesWithPaging(QueryParameters queryParameters);
        Task<Surtax> GetSurtaxByIdAsync(int id);
        Task CreateSurtax(Surtax surtax);
        Task UpdateSurtax(Surtax surtax);
        Task DeleteSurtax(Surtax surtax);
    }
}