using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Stock : BaseEntity
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public string CompanyName { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        [ForeignKey("Modality")]
        public int ModalityId { get; set; }
        public Modality Modality { get; set; }


        [ForeignKey("Segment")]
        public int SegmentId { get; set; }
        public Segment Segment { get; set; }


        [ForeignKey("TypeOfStock")]
        public int TypeOfStockId { get; set; }
        public TypeOfStock Type { get; set; }

        public ICollection<StockTransaction> StockTransactions { get; set; }

        public int? NumberOfEmployees { get; set; }
        public int? SharesOutstanding { get; set; }
        public int? OwnShares { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? EnterpriseValue { get; set; }
        public decimal? Dividend { get; set; }
    }
}