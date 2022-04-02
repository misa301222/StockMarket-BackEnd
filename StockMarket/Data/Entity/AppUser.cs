using Microsoft.AspNetCore.Identity;

namespace StockMarket.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
    }
}
