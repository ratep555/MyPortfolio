using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStockService
    {
        Task<Stock> GetStockByIdAsync(int id);
        Task<IQueryable<Stock>> GetStocksWithSearching(QueryParameters queryParameters);
        Task<IQueryable<Stock>> GetStocksWithPaging(QueryParameters queryParameters);
        Task CreateStock(Stock stock);
        Task UpdateStock(Stock stock);
        Task DeleteStock(Stock stock);
        Task<List<Category>> GetCategories();
        Task<List<Modality>> GetModalities();
        Task<List<Segment>> GetSegments();
        Task<List<TypeOfStock>> GetTypesOfStock();
        Task RefreshPrices(string symbol, decimal price);

       

    }
}