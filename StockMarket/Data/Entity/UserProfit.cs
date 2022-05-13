using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class UserProfit
    {
        public UserProfit(string email, decimal money)
        {
            Email = email;
            Money = money;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        [Required]
        public decimal Money { get; set; }
    }
}
