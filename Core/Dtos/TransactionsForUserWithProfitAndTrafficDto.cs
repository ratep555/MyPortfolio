using System.Collections.Generic;

namespace Core.Dtos
{
    public class TransactionsForUserWithProfitAndTrafficDto
    {
        public IEnumerable<TransactionForUserDto> ListOfTransactions { get; set; }
        public decimal? TotalNetProfit { get; set; }
        public decimal? TotalTraffic { get; set; }
    }
}