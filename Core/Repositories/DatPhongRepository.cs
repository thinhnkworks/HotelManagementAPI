using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class DatPhongRepository : GenericRepository<SuKienDatPhong>, IDatPhongRepository
    {
        public DatPhongRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(SuKienDatPhong entity)
        {
            try
            {
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(DatPhongRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<SuKienDatPhong>> GetAllAsync()
        {
            try
            {
                var datPhongs = await base.GetAllAsync();
                return datPhongs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(DatPhongRepository));
                return new List<SuKienDatPhong>();
            }
        }
        public override async Task<SuKienDatPhong?> GetAsync(int id)
        {
            try
            {
                SuKienDatPhong? datPhong = await base.GetAsync(id);
                if (datPhong == null)
                    return null;
                return datPhong;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(DatPhongRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var datPhongExist = await _dbSet.FindAsync(id);

                if (datPhongExist == null)
                    return false;
                _dbSet.Remove(datPhongExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(DatPhongRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, SuKienDatPhong entity)
        {
            try
            {
                var datPhongExist = await _dbSet.FirstOrDefaultAsync(x => x.MaSk == id);

                if (datPhongExist == null)
                {
                    return await AddAsync(entity);
                }
               
                if (entity.SoNgayO > 0)
                {
                    datPhongExist.SoNgayO = entity.SoNgayO;
                }
                if (entity.MaNv > 0)
                    datPhongExist.MaNv = entity.MaNv;
                if(entity.MaPhong > 0)
                    datPhongExist.MaPhong = entity.MaPhong;
                if (entity.MaKh > 0)
                    datPhongExist.MaKh = entity.MaKh;
                if(entity.NgayNhanPhong != null)
                {
                    datPhongExist.NgayNhanPhong = entity.NgayNhanPhong;
                }
                if (entity.NgayTraPhong != null)
                    datPhongExist.NgayTraPhong = entity.NgayTraPhong;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(DatPhongRepository));
                return false;
            }
        }

        public async Task<double> TienPhong(int MaSK)
        {
            try
            {
                var tienPhong = await (
                           from SKDatPhong in _dataContext.SuKienDatPhongs
                           join Phong in _dataContext.Phongs
                           on SKDatPhong.MaPhong equals Phong.MaPhong
                           join LoaiPhong in _dataContext.LoaiPhongs
                           on Phong.MaLoaiPhong equals LoaiPhong.MaLoaiPhong
                           where SKDatPhong.MaSk == MaSK
                            select LoaiPhong.Gia
                           ).FirstOrDefaultAsync();

                return tienPhong;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} tien Phong method error", typeof(DatPhongRepository));
                return 0;
            }
        }
    }
}
