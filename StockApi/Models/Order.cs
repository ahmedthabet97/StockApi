using StockApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderType Type { get; set; }
        public int Quantity { get; set; }
        public List<Stock>? Stocks { get; set; }
    }
}
