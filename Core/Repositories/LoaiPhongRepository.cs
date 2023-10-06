using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class LoaiPhongRepository : GenericRepository<LoaiPhong>, ILoaiPhongRepository
    {
        public LoaiPhongRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(LoaiPhong entity)
        {
            try
            {
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(LoaiPhongRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<LoaiPhong>> GetAllAsync()
        {
            try
            {
                var loaiPhongs = await _dbSet.Include(x => x.Phongs).ToListAsync();
                return loaiPhongs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(LoaiPhongRepository));
                return new List<LoaiPhong>();
            }
        }
        public override async Task<LoaiPhong?> GetAsync(int id)
        {
            try
            {
                LoaiPhong? loaiPhong = await _dbSet.Include(x => x.Phongs).FirstOrDefaultAsync(x => x.MaLoaiPhong == id);
                if (loaiPhong == null)
                    return null;
                return loaiPhong;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(LoaiPhongRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var loaiPhongExist = await _dbSet.FindAsync(id);

                if (loaiPhongExist == null)
                    return false;
                _dbSet.Remove(loaiPhongExist);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(LoaiPhongRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, LoaiPhong entity)
        {
            try
            {
                var loaiPhongExist = await _dbSet.FirstOrDefaultAsync(x => x.MaLoaiPhong == id);

                if (loaiPhongExist == null)
                {
                    return await AddAsync(entity);
                }
                if (!String.IsNullOrEmpty(entity.TenLoaiPhong))
                {
                    loaiPhongExist.TenLoaiPhong= entity.TenLoaiPhong;
                }
                if (entity.SoNguoiO < 0)
                {
                    loaiPhongExist.SoNguoiO = entity.SoNguoiO;
                }
                if (entity.Gia < 0.0)
                {
                    loaiPhongExist.Gia = entity.Gia;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(LoaiPhongRepository));
                return false;
            }
        }
    }
}
