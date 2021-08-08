using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class StockTransaction : BaseEntity
    {
        [ForeignKey("Stock")]
        public int StockId { get; set; }       
        public Stock Stock { get; set; }   


        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public bool Purchase { get; set; }
        public int Resolved { get; set; }
        public string Email { get; set; }

    }
}

