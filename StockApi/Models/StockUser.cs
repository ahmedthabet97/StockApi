namespace StockApi.Models
{
    public class StockUser:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }=DateTime.UtcNow;
    } 
}
