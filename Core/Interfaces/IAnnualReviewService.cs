using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAnnualReviewService
    {
        Task<IQueryable<AnnualProfitOrLoss>> GetAnnualProfitOrLossWithSearching(QueryParameters queryParameters);     
        Task<IQueryable<AnnualProfitOrLoss>> GetAnnualProfitOrLossWithPaging(QueryParameters queryParameters);      
        Task ActionsRegardingProfitOrLossCardUponLogin(string email);
        Task ActionsRegardingProfitOrLossCardUponPurchase(string email);
        Task ActionsRegardingProfitOrLossCardUponSelling(string email);
        Task TwoYearException(string email, StockTransaction transaction);
        Task<AnnualTaxLiabilityDto> ShowTaxLiability(string email, int id);
        Task<AnnualTaxLiabilityDto> ShowAnnual(string email);
        Task<IEnumerable<AnnualProfitOrLoss>> ShowListOfAnnualProfitAndLoss(string email);
    }
}