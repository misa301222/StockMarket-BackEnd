using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class TradeStockHistory
    {
        public TradeStockHistory(int tradeStockHistoryId, string sourceEmail, string destinyEmail, string stockName, int stockQuantity, decimal stockPrice, DateTime transactionDate, string status)
        {
            TradeStockHistoryId = tradeStockHistoryId;
            SourceEmail = sourceEmail;
            DestinyEmail = destinyEmail;
            StockName = stockName;
            StockQuantity = stockQuantity;
            StockPrice = stockPrice;
            TransactionDate = transactionDate;
            Status = status;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TradeStockHistoryId { get; set; }
        [Required]
        public string SourceEmail { get; set; }
        [Required]
        public string DestinyEmail { get; set; }
        public string StockName { get; set; }
        public int StockQuantity { get; set; }
        public decimal StockPrice { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
