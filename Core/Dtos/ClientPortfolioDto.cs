namespace Core.Dtos
{
    public class ClientPortfolioDto
    {
        public int StockId { get; set; }
        public int TransactionId { get; set; }
        public string Symbol { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPriceOfPurchasePerStock { get; set; }
        public decimal TotalMarketValuePerStock { get; set; }
        public decimal AveragePriceOfPurchase { get; set; }
        public decimal CurrentPrice { get; set; }
        public string Email { get; set; }
        public decimal? PortfolioPercentage { get; set; }
    }
}