namespace StockMarket.Models.DTO
{
    public class UserDTO
    {
        public UserDTO(string fullname, string email, string username, DateTime datecreated, List<String> roles)
        {
            this.FullName = fullname;
            this.Email = email;
            this.UserName = username;
            this.DateCreated = datecreated;
            this.Roles = roles;
        }

        public String FullName { get; set; }
        public String Email { get; set; }
        public String UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public String Token { get; set; }
        public List<String> Roles { get; set; }
    }
}
