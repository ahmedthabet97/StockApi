
using Microsoft.EntityFrameworkCore;

namespace StockApi.Models;

public class ApplicationDbContext : IdentityDbContext<StockUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>()
            .HasMany(o => o.Orders)
            .WithMany(o => o.Stocks)
            .UsingEntity(e=>e.ToTable("StockOrders"));
        base.OnModelCreating(modelBuilder);
    }
}
