using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApi.Models;

public class Stock
{
    [Key]
    public string Symbol { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime Timestamp { get; set; }
    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
}
