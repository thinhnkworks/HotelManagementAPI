using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class ThemDichVuRepository: GenericRepository<SuKienSuDungDichVu>, IThemDichVuRepository
    {
        public ThemDichVuRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(SuKienSuDungDichVu entity)
        {
            try
            {
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(ThemDichVuRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<SuKienSuDungDichVu>> GetAllAsync()
        {
            try
            {
                var themDichVus = await (from SKSuDungDichVu in _dbSet
                                  where SKSuDungDichVu.MaDvNavigation != null && SKSuDungDichVu.MaNvNavigation != null && SKSuDungDichVu.MaSkdpNavigation != null
                                  select SKSuDungDichVu).ToListAsync();
                                  
                return themDichVus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(ThemDichVuRepository));
                return new List<SuKienSuDungDichVu>();
            }
        }
        public override async Task<SuKienSuDungDichVu?> GetAsync(int id)
        {
            try
            {
                SuKienSuDungDichVu? themDichVu= await base.GetAsync(id);
                if (themDichVu == null)
                    return null;
                return themDichVu;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(ThemDichVuRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var themDichVuExist = await _dbSet.FindAsync(id);

                if (themDichVuExist == null)
                    return false;
                _dbSet.Remove(themDichVuExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(ThemDichVuRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, SuKienSuDungDichVu entity)
        {
            try
            {
                var themDichVuExist = await _dbSet.FirstOrDefaultAsync(x => x.MaSk == id);

                if (themDichVuExist == null)
                {
                    return await AddAsync(entity);
                }
                if (entity.MaNv > 0)
                {
                    themDichVuExist.MaNv = entity.MaNv;
                }
                if (entity.SoLuong > 0)
                    themDichVuExist.SoLuong = entity.SoLuong;
                if (entity.MaDv > 0)
                    themDichVuExist.MaDv = entity.MaDv;
                if (entity.MaSkdp > 0)
                    themDichVuExist.MaSkdp = entity.MaSkdp;
                if (entity.ThoiGian != null)
                    themDichVuExist.ThoiGian = entity.ThoiGian;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(ThemDichVuRepository));
                return false;
            }
        }

        public async Task<double> TongTienDichVu(SuKienSuDungDichVu ThemDichVu)
        {
            try
            {
                var tienDichVu = await (from skDichVu in _dataContext.SuKienSuDungDichVus
                                        join DichVu in _dataContext.DichVus
                                        on skDichVu.MaDv equals DichVu.MaDv
                                        where skDichVu.MaSk == ThemDichVu.MaSk
                                        select DichVu.Gia).SingleOrDefaultAsync();
                return tienDichVu * ThemDichVu.SoLuong;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} TongTienPhuPhi method error", typeof(ThemDichVuRepository));
                return 0.0;
            }
        }

        public async Task<IEnumerable<SuKienSuDungDichVu>> DanhSachDichVuTheoPhongVaMaSK(int? MaSKDatPhong, int? MaPhong)
        {
            try
            {
                var danhSachThemDichVu = await GetAllAsync();
                if(MaSKDatPhong.HasValue && MaPhong.HasValue)
                {
                    danhSachThemDichVu = await (from skThemDichVu in _dbSet
                                                join sKDatPhong in _dataContext.SuKienDatPhongs
                                                on skThemDichVu.MaSkdp equals sKDatPhong.MaSk
                                                where skThemDichVu.MaDvNavigation != null && skThemDichVu.MaNvNavigation != null && skThemDichVu.MaSkdpNavigation != null
                                                        && skThemDichVu.MaSkdp == MaSKDatPhong && sKDatPhong.MaPhong == MaPhong
                                                select skThemDichVu).ToListAsync();
                }
                return danhSachThemDichVu;
            }catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} DanhSachDichVuTheoPhongVaMaSK method error", typeof(ThemDichVuRepository));
                return new List<SuKienSuDungDichVu>();
            }
        }
    }
}
