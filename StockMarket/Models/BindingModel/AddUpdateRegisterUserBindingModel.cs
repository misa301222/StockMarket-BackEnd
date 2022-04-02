namespace StockMarket.Models
{

    public class AddUpdateRegisterUserBindingModel
    {
        public String FullName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public List<String> Roles { get; set; }

    }
}
