using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class StockBought
    {
        public StockBought(int stocksBoughtId, string email, string stockName, int quantityBought, decimal transactionTotal, DateTime transactionDate)
        {
            StocksBoughtId = stocksBoughtId;
            Email = email;
            StockName = stockName;
            QuantityBought = quantityBought;
            TransactionTotal = transactionTotal;
            TransactionDate = transactionDate;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StocksBoughtId { get; set; }
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string StockName { get; set; }
        public int QuantityBought { get; set; }
        public decimal TransactionTotal { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime TransactionDate { get; set; }
    }
}
