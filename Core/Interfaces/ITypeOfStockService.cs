using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITypeOfStockService
    {
        Task<List<TypeOfStock>> GetTypesOfStockWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForTypesOfStock();
        Task<TypeOfStock> GetTypeOfStockByIdAsync(int id);
        Task CreateTypeOfStock(TypeOfStock typeOfStock);
        Task UpdateTypeOfStock(TypeOfStock typeOfStock);       
        Task DeleteTypeOfStock(TypeOfStock typeOfStock);
       
    }
}