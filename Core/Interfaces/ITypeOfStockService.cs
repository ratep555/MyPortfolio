using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITypeOfStockService
    {
        Task<IQueryable<TypeOfStock>> GetTypesOfStockAsync(QueryParameters queryParameters);
        Task<TypeOfStock> GetTypeOfStockByIdAsync(int id);
        Task CreateTypeOfStock(TypeOfStock typeOfStock);
        Task UpdateTypeOfStock(TypeOfStock typeOfStock);       
        Task DeleteTypeOfStock(TypeOfStock typeOfStock);
       
    }
}