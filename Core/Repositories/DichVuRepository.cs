using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class DichVuRepository: GenericRepository<DichVu>, IDichVuRepository
    {
        public DichVuRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(DichVu entity)
        {
            try
            {
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(DichVuRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<DichVu>> GetAllAsync()
        {
            try
            {
                var dichVus = await base.GetAllAsync();
                return dichVus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(DichVuRepository));
                return new List<DichVu>();
            }
        }
        public override async Task<DichVu?> GetAsync(int id)
        {
            try
            {
                DichVu? dichVu = await base.GetAsync(id);
                if (dichVu == null)
                    return null;
                return dichVu;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(DichVuRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dichVuExist = await _dbSet.FindAsync(id);

                if (dichVuExist == null)
                    return false;
                _dbSet.Remove(dichVuExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(DichVuRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, DichVu entity)
        {
            try
            {
                var dichVuExist = await _dbSet.FirstOrDefaultAsync(x => x.MaDv == id);

                if (dichVuExist == null)
                {
                    return await AddAsync(entity);
                }
                if (!String.IsNullOrEmpty(entity.TenDv))
                {
                    dichVuExist.TenDv = entity.TenDv;
                }
                if (entity.Gia >= 0)
                {
                    dichVuExist.Gia = entity.Gia;
                }
                dichVuExist.TrangThai = entity.TrangThai;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(DichVuRepository));
                return false;
            }
        }
    }
}
