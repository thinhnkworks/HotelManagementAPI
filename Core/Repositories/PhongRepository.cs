using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class PhongRepository : GenericRepository<Phong>, IPhongRepository
    {
        public PhongRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }

        public override async Task<bool> AddAsync(Phong entity)
        {
            try
            {
                var existLoaiPhong = await _dataContext.LoaiPhongs.FindAsync(entity.MaLoaiPhong);
                if (existLoaiPhong == null)
                    return false;
                var newphong = entity;
                newphong.MaLoaiPhongNavigation = existLoaiPhong;
                return await base.AddAsync(newphong);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(PhongRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<Phong>> GetAllAsync()
        {
            try
            {
                var phongs = await _dbSet.Include(x => x.MaLoaiPhongNavigation).ToListAsync();
                return phongs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(PhongRepository));
                return new List<Phong>();
            }
        }
        public override async Task<Phong?> GetAsync(int id)
        {
            try
            {
                Phong? phong = await _dbSet.Include(x => x.MaLoaiPhongNavigation).FirstOrDefaultAsync(x => x.MaPhong == id);
                if (phong == null)
                    return null;
                return phong;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(PhongRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var phongExist = await _dbSet.FindAsync(id);

                if (phongExist == null)
                    return false;
                _dbSet.Remove(phongExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(PhongRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, Phong entity)
        {
            try
            {
                var phongExist = await _dbSet.FirstOrDefaultAsync(x => x.MaPhong == id);

                if (phongExist == null)
                {
                    return await AddAsync(entity);
                }
                if (entity.MaLoaiPhong != null)
                {
                    var existLoaiPhong = await _dataContext.LoaiPhongs.FindAsync(entity.MaLoaiPhong);
                    if (existLoaiPhong != null)
                    {
                        phongExist.MaLoaiPhong = entity.MaLoaiPhong;
                        phongExist.MaLoaiPhongNavigation = existLoaiPhong;
                    }
                    else
                        return false;
                }
                phongExist.TrangThai = entity.TrangThai;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(NhanVienRepository));
                return false;
            }
        }

        public async Task<double> tienPhong(int id)
        {
            var tienPhong = await (from phongs in _dataContext.Phongs
                            join loaiPhongs in _dataContext.LoaiPhongs
                            on phongs.MaLoaiPhong equals loaiPhongs.MaLoaiPhong
                            where phongs.MaPhong == id
                            select loaiPhongs.Gia).FirstOrDefaultAsync();
            return tienPhong;
        }

        public async Task<bool> DoiTrangThai(int id, int trangthai)
        {
            Phong? phong = await GetAsync(id);
            if(phong == null) return false;
            phong.TrangThai = trangthai;
            return true;
        }
    }
}
