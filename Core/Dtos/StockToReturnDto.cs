namespace Core.Dtos
{
    public class StockToReturnDto
    {
        public int Id { get; set; }
        public string Symbol { get; set;}
        public decimal CurrentPrice { get; set; }
        public string CompanyName { get; set; }
        public int? TotalQuantity { get; set; }
        public string Category { get; set; }
        public string Modality { get; set; }
        public string Segment { get; set; }
        public string TypeOfStock { get; set; }
        public int? NumberOfEmployees { get; set; }
        public int? SharesOutstanding { get; set; }
        public int? OwnShares { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? EnterpriseValue { get; set; }
        public decimal? Dividend { get; set; }
    }
}