namespace Core.Dtos
{
    public class AnnualProfitOrLossDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TaxableIncome { get; set; }
        public string Email { get; set; }
    }
}