namespace StockApi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderType Type { get; set; }
        public int Quantity { get; set; }
        public List<Stock>? Stocks { get; set; }
    }
}
