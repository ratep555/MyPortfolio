using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStockService
    {
        Task<Stock> GetStockByIdAsync(int id);
        Task<IEnumerable<Stock>> GetStocksAsync();


    }
}