using System.Collections.Generic;

namespace Core.Dtos
{
    public class ClientPortfolioWithProfitOrLossDto
    {
        public IEnumerable<ClientPortfolioDto> ClientPortfolios { get; set; }
        public decimal TotalPriceOfPurchase { get; set; }
        public decimal TotalMarketValue { get; set; }
    }
}