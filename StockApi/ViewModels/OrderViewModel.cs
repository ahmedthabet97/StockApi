using StockApi.Enums;
using StockApi.Models;

namespace StockApi.ViewModels
{
    public class OrderViewModel
    {
        public List<string> StockSymbol { get; set; }
        public OrderType OrderType { get; set; }
        public int Quantity { get; set; }
    }
}
