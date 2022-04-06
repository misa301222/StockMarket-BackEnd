using System.ComponentModel.DataAnnotations;

namespace StockMarket.Data.Entity
{
    public class UserPortfolio
    {
        public UserPortfolio(string email, string stockName, int stockQuantity, decimal stockPrice)
        {
            Email = email;
            StockName = stockName;
            StockQuantity = stockQuantity;
            StockPrice = stockPrice;
        }

        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string StockName { get; set; }
        public int StockQuantity { get; set; }
        [Required]
        public decimal StockPrice { get; set; }
    }
}
