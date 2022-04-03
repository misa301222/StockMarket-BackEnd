using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class Stock
    {
        public Stock(string stockName, string stockDescription, decimal stockPrice, int stockQuantity, string stockLogoURL, DateTime dateAdded, string stockOwner)
        {
            StockName = stockName;
            StockDescription = stockDescription;
            StockPrice = stockPrice;
            StockQuantity = stockQuantity;
            StockLogoURL = stockLogoURL;
            DateAdded = dateAdded;
            StockOwner = stockOwner;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(40)]
        public string StockName { get; set; }
        public string StockDescription { get; set; }
        [Required]
        public decimal StockPrice { get; set; }
        public int StockQuantity { get; set; }
        public string StockLogoURL { get; set; }
        public DateTime DateAdded { get; set; }
        [Required]
        public string StockOwner { get; set; }

    }
}
