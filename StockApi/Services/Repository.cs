
using Microsoft.EntityFrameworkCore;
using StockApi.Models;

namespace StockApi.Services;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task DeleteAsync(object id)
    {
        var entity = await GetAsync(id);
        _context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync(string? include = "")
    {
        var query = _context.Set<T>().AsQueryable();
        if (!string.IsNullOrEmpty(include))
        {
            query = query.Include(include);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetAsync(object id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        return result!;
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }
}
