using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class StockHistory
    {
        public StockHistory(int stockId, string stockName, DateTime stockDate, decimal stockPrice)
        {
            StockId = stockId;
            StockName = stockName;
            StockDate = stockDate;
            StockPrice = stockPrice;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        [Required]
        public string StockName { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime StockDate { get; set; }
        public decimal StockPrice { get; set; }
    }
}
