using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITransactionService
    {
        Task<StockTransaction> BuyStockAsync(TransactionDto transactionDto, int id, string email);
        Task<StockTransaction> SellStockAsync(TransactionDto transactionDto, int id, string email);
        Task UpdateResolved(int stockId, string email);
        Task<IEnumerable<ClientPortfolioDto>> ShowClientPortfolio(QueryParameters queryParameters, string email);
        Task<ClientPortfolioWithProfitOrLossDto> ClientPortfolioWithProfitOrLoss(
        QueryParameters queryParameters,
        string email);
        Task<TransactionsForUserWithProfitAndTrafficDto> ShowTransactionsWithProfitAndTraffic(
        QueryParameters queryParameters,
        string email);
        Task<int> TotalQuantity(string email, int stockId);
        
    }
}