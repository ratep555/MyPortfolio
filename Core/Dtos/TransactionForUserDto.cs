using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class TransactionForUserDto
    {
        public int Id { get; set; }
        public int StockId { get; set; }        
        public string Stock { get; set; }   


        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public bool Purchase { get; set; }
        public int Resolved { get; set; }
        public string Email { get; set; }
        public decimal? NetProfit { get; set; }
    }
}