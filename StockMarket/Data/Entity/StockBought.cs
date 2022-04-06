using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class StockBought
    {
        public StockBought(int stocksBoughtId, string email, string stockName, int quantityBought)
        {
            StocksBoughtId = stocksBoughtId;
            Email = email;
            StockName = stockName;
            QuantityBought = quantityBought;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int StocksBoughtId { get; set; }        
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string StockName { get; set; }
        public int QuantityBought { get; set; }
    }
}
