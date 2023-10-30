using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class HoaDonRepository: GenericRepository<HoaDon>, IHoaDonRepository
    {
        public HoaDonRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(HoaDon entity)
        {
            try
            {
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(HoaDonRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<HoaDon>> GetAllAsync()
        {
            try
            {
                var hoaDons = await base.GetAllAsync();
                return hoaDons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(HoaDonRepository));
                return new List<HoaDon>();
            }
        }
        public override async Task<HoaDon?> GetAsync(int id)
        {
            try
            {
                HoaDon? hoaDon = await base.GetAsync(id);
                if (hoaDon == null)
                    return null;
                return hoaDon;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(HoaDonRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var hoaDonExist = await _dbSet.FindAsync(id);

                if (hoaDonExist == null)
                    return false;
                _dbSet.Remove(hoaDonExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(HoaDonRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, HoaDon entity)
        {
            try
            {
                var hoaDonExist = await _dbSet.FirstOrDefaultAsync(x => x.MaHd == id);

                if (hoaDonExist == null)
                {
                    return await AddAsync(entity);
                }
                if (entity.MaSkdp != hoaDonExist.MaSkdp && entity.MaSkdp > 0)
                {
                    hoaDonExist.MaSkdp = entity.MaSkdp;
                }
                if (entity.TriGiaHd > 0)
                    hoaDonExist.TriGiaHd = entity.TriGiaHd;
                if (hoaDonExist.DaThanhToan != entity.DaThanhToan)
                    hoaDonExist.DaThanhToan = entity.DaThanhToan;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(HoaDonRepository));
                return false;
            }
        }

        public async Task UpdateTien(int id, double soTien)
        {
            try
            {
                HoaDon? hoaDon = await _dbSet.FirstOrDefaultAsync(x => x.MaSkdp == id);
                if(hoaDon !=null)
                {
                    hoaDon.TriGiaHd += soTien;
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} updateTien method error", typeof(HoaDonRepository));
            }
        }
    }
}
