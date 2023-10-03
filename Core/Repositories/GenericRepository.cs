using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DataContext _dataContext;
        protected DbSet<T> _dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(DataContext dataContext, ILogger logger)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
            _logger = logger;
        }
        public virtual async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> DeleteAsync(int id)
        {
           throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
           return  await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            T? user = await _dbSet.FindAsync(id);
            return user;
        }

        public virtual Task<bool> UpdateAsync(int id, T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> CheckUniqueOfStd(string sdt)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> CheckUniqueOfCccd(string cccd)
        {
            throw new NotImplementedException();
        }
    }
}
