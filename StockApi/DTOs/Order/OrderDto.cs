namespace StockApi.DTOs.Order
{
    public class OrderDto
    {
        public List<string> StockSymbol { get; set; }
        public OrderType OrderType { get; set; }
        public int Quantity { get; set; }
    }
}
