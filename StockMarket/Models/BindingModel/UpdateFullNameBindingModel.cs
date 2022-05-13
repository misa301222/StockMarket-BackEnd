namespace StockMarket.Models.BindingModel
{
    public class UpdateFullNameBindingModel
    {
        public UpdateFullNameBindingModel(string email, string fullName)
        {
            Email = email;
            FullName = fullName;
        }

        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
