using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class CustomerRepository : GenericRepository<KhachHang>, ICustomerRepository
    {
        public CustomerRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(KhachHang entity)
        {
            try
            {
                if (!(await CheckUniqueOfStd(entity.Sdt!)) || !(await CheckUniqueOfCccd(entity.Cccd!)))
                    return false;
                return await base.AddAsync(entity);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(CustomerRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<KhachHang>> GetAllAsync()
        {
            try
            {
               var khachHangs =  await base.GetAllAsync();
                return khachHangs;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(CustomerRepository));
                return new List<KhachHang>();
            }
        }
        public override async Task<KhachHang?> GetAsync(int id)
        {
            try
            {
                KhachHang? khachHang = await base.GetAsync(id);
                if(khachHang == null)
                    return null;
                return khachHang;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(CustomerRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var khachHangExist = await _dbSet.FindAsync(id);

                if(khachHangExist == null)
                    return false;
                _dbSet.Remove(khachHangExist);
                return true;   

            } catch (Exception ex) {
                _logger.LogError(ex, "{Repo} delete method error", typeof(CustomerRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, KhachHang entity)
        {
            try
            {
                var khachHangExist = await _dbSet.FirstOrDefaultAsync(x => x.MaKh == id);

                if (khachHangExist == null)
                {
                    return await AddAsync(entity);
                }
                if ((!(await CheckUniqueOfStd(entity.Sdt!)) && khachHangExist.Sdt != entity.Sdt) || (!(await CheckUniqueOfCccd(entity.Cccd!)) && khachHangExist.Cccd != entity.Cccd))
                    return false;
                if (!String.IsNullOrEmpty(entity.HoTen))
                {
                    khachHangExist.HoTen = entity.HoTen;
                }
                if (entity.SoLanNghi != null)
                {
                    khachHangExist.SoLanNghi = entity.SoLanNghi;
                }
                if (entity.NgaySinh != null)
                {
                    khachHangExist.NgaySinh = entity.NgaySinh;
                }
                if (!String.IsNullOrEmpty(entity.GioiTinh))
                {
                    khachHangExist.GioiTinh = entity.GioiTinh;
                }
                if (!String.IsNullOrEmpty(entity.DiaChi))
                {
                    khachHangExist.DiaChi = entity.DiaChi;
                }
                if (!String.IsNullOrEmpty(entity.Sdt))
                {
                    khachHangExist.Sdt = entity.Sdt;
                }
                if (!String.IsNullOrEmpty(entity.Cccd))
                {
                    khachHangExist.Cccd = entity.Cccd;
                }
                return true;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(CustomerRepository));
                return false;
            }
        }
        public async Task<bool> CheckUniqueOfStd(string sdt)
        {
            try
            {
                var exitKhachHang = await _dbSet.FirstOrDefaultAsync(x => x.Sdt == sdt);
                if (exitKhachHang == null)
                    return true;
                return false;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} CheckSDT method error", typeof(CustomerRepository));
                return false;
            }
        }

        public  async Task<bool> CheckUniqueOfCccd(string cccd)
        {
            try
            {
                var exitKhachHang = await _dbSet.FirstOrDefaultAsync(x => x.Cccd == cccd);
                if (exitKhachHang == null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} CheckCCCD method error", typeof(CustomerRepository));
                return false;
            }
        }

        public async Task<KhachHang?> GetByCCCDAsync(string cccd)
        {
            try
            {
                var existKhachHang = await _dbSet.FirstOrDefaultAsync(x => x.Cccd == cccd);
                if (existKhachHang != null)
                    return existKhachHang;
                return null;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} getByCCCD method error", typeof(CustomerRepository));
                return null;
            }
        }

        public async Task<bool> TangNgayNghi(int id)
        {
            KhachHang? khachHang = await GetAsync(id);
            if (khachHang == null) return false;
            khachHang.SoLanNghi += 1;
            return true;
        }
    }
}
