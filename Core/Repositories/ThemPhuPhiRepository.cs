using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class ThemPhuPhiRepository: GenericRepository<SuKienThemPhuPhi>, IThemPhuPhiRepository
    {
        public ThemPhuPhiRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(SuKienThemPhuPhi entity)
        {
            try
            {
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(ThemPhuPhiRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<SuKienThemPhuPhi>> GetAllAsync()
        {
            try
            {
                var themPhuPhis = await (from SKThemPhuPhi in _dbSet
                                         where SKThemPhuPhi.MaPpNavigation != null && SKThemPhuPhi.MaNvNavigation != null && SKThemPhuPhi.MaSkdpNavigation != null
                                         select SKThemPhuPhi).ToListAsync();
                return themPhuPhis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(ThemPhuPhiRepository));
                return new List<SuKienThemPhuPhi>();
            }
        }
        public override async Task<SuKienThemPhuPhi?> GetAsync(int id)
        {
            try
            {
                SuKienThemPhuPhi? themPhuPhi = await base.GetAsync(id);
                if (themPhuPhi == null)
                    return null;
                return themPhuPhi;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(ThemPhuPhiRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var themPhuPhiExist = await _dbSet.FindAsync(id);

                if (themPhuPhiExist == null)
                    return false;
                _dbSet.Remove(themPhuPhiExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(ThemPhuPhiRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, SuKienThemPhuPhi entity)
        {
            try
            {
                var themPhuPhiExist = await _dbSet.FirstOrDefaultAsync(x => x.MaSk == id);

                if (themPhuPhiExist == null)
                {
                    return await AddAsync(entity);
                }
                if (entity.MaNv > 0)
                {
                    themPhuPhiExist.MaNv = entity.MaNv;
                }
                if (entity.SoLuong > 0)
                    themPhuPhiExist.SoLuong = entity.SoLuong;
                if (entity.MaPp > 0)
                    themPhuPhiExist.MaPp = entity.MaPp;
                if (entity.MaSkdp > 0)
                    themPhuPhiExist.MaSkdp = entity.MaSkdp;
                if(entity.ThoiGian != null)
                    themPhuPhiExist.ThoiGian = entity.ThoiGian;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(ThemPhuPhiRepository));
                return false;
            }
        }

        public async Task<double> TongTienPhuPhi(SuKienThemPhuPhi ThemPhuPhi)
        {
            try
            {
                var tienDichVu = await (from skDichVu in _dataContext.SuKienThemPhuPhis
                                        join DichVu in _dataContext.PhuPhis
                                        on skDichVu.MaPp equals DichVu.MaPp
                                        where skDichVu.MaSk == ThemPhuPhi.MaSk
                                        select DichVu.Gia).SingleOrDefaultAsync();
                return tienDichVu * ThemPhuPhi.SoLuong;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} TongTienPhuPhi method error", typeof(ThemPhuPhiRepository));
                return 0.0;
            }
        }

        public async Task<IEnumerable<SuKienThemPhuPhi>> DanhSachPhuPhiTheoPhongVaMaSK(int? MaSKDatPhong)
        {
            try
            {
                var danhSachThemPhuPhi = await GetAllAsync();
                if (MaSKDatPhong.HasValue)
                {
                    danhSachThemPhuPhi = await(from skThemPhuPhi in _dbSet
                                               join sKDatPhong in _dataContext.SuKienDatPhongs
                                               on skThemPhuPhi.MaSkdp equals sKDatPhong.MaSk
                                               where skThemPhuPhi.MaPpNavigation != null && skThemPhuPhi.MaNvNavigation != null && skThemPhuPhi.MaSkdpNavigation != null
                                                       && skThemPhuPhi.MaSkdp == MaSKDatPhong
                                               select skThemPhuPhi).ToListAsync();
                }
                return danhSachThemPhuPhi;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} DanhSachPhuPhiTheoPhongVaMaSK method error", typeof(ThemPhuPhiRepository));
                return new List<SuKienThemPhuPhi>();
            }
        }
    }
}
