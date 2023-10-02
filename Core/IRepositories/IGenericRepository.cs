namespace HotelManagementAPI.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(int id,T entity);
        Task<bool> DeleteAsync(int id);
    }
}
