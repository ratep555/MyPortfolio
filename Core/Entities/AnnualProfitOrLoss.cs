namespace Core.Entities
{
    public class AnnualProfitOrLoss : BaseEntity
    {
        public int Year { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TaxableIncome { get; set; }
        public decimal? TaxExemption { get; set; }
        public string Email { get; set; }
        public bool Locked { get; set; }
    }
}