namespace StockApi.DTOs.Stock
{
    public class GetStockDto
    {
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
