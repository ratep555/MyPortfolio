namespace Core.Dtos
{
    public class StockToEditDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public string CompanyName { get; set; }
        public int CategoryId { get; set; }
        public int ModalityId { get; set; }
        public int SegmentId { get; set; }
        public int TypeOfStockId { get; set; }
        public int? NumberOfEmployees { get; set; }
        public int? SharesOutstanding { get; set; }
        public int? OwnShares { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? EnterpriseValue { get; set; }
        public decimal? Dividend { get; set; }
    }
}