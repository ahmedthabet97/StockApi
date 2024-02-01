using Microsoft.EntityFrameworkCore;

namespace StockApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Order>()
            .HasMany<Stock>()
            .WithOne()
            .HasForeignKey(o => o.OrderId);
        base.OnModelCreating(modelBuilder);
    }
}
