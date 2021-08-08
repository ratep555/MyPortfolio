using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAnnualReviewService
    {
        Task ActionsRegardingProfitOrLossCardUponLogin(string email);
        Task ActionsRegardingProfitOrLossCardUponPurchase(string email);
        Task ActionsRegardingProfitOrLossCardUponSelling(string email);
        Task TwoYearException(string email, StockTransaction transaction);
    }
}