using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace StockApi.Models;

public class Stock
{
    [Key]
    public string Symbol { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime Timestamp { get; set; }
   public IEnumerable<Order>? Orders { get; set; }
}
