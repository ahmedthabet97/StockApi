using StockApi.Models;

namespace StockApi.Services;

public interface IUnitOfWork
{
    IRepository<Order> OrderRepository { get; }
    IRepository<Stock> StockRepository { get; }
    Task<int> SaveChangesAsync();
}
