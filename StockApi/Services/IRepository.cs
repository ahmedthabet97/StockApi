namespace StockApi.Services
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(object id);
        void Update(T entity);
        Task DeleteAsync(object id);
        Task<IEnumerable<T>> GetAllAsync(string? include="");
        Task CreateAsync(T entity);

    }
}
