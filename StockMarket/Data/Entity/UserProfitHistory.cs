using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class UserProfitHistory
    {
        public UserProfitHistory(int userProfitId, string email, decimal money, DateTime transactionDate)
        {
            UserProfitId = userProfitId;
            Email = email;
            Money = money;
            TransactionDate = transactionDate;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserProfitId { get; set; }
        public string Email { get; set; }
        public decimal Money { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime TransactionDate { get; set; }
    }
}
