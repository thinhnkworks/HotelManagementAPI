using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.Helper;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class NhanVienRepository : GenericRepository<NhanVien>, INhanVienRepository
    {
        private readonly IHelper _helper;
        private readonly IConfiguration _configuration;
        public NhanVienRepository(DataContext dataContext, ILogger logger, IHelper helper, IConfiguration configuration) : base(dataContext, logger)
        {
            _helper = helper;
            _configuration = configuration;
        }
        public override async Task<bool> AddAsync(NhanVien entity)
        {
            try
            {
                if (!(await CheckUniqueOfStd(entity.Sdt!)) || !(await CheckUniqueOfCccd(entity.Cccd!)))
                    return false;
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(NhanVienRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            try
            {
                var nhanviens = await base.GetAllAsync();
                return nhanviens;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(NhanVienRepository));
                return new List<NhanVien>();
            }
        }
        public override async Task<NhanVien?> GetAsync(int id)
        {
            try
            {
                NhanVien? nhanVien = await base.GetAsync(id);
                if (nhanVien == null)
                    return null;
                return nhanVien;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(NhanVienRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var khachHangExist = await _dbSet.FindAsync(id);

                if (khachHangExist == null)
                    return false;
                _dbSet.Remove(khachHangExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(NhanVienRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, NhanVien entity)
        {
            try
            {
                var nhanVienExist = await _dbSet.FirstOrDefaultAsync(x => x.MaNv == id);

                if (nhanVienExist == null)
                {
                    return await AddAsync(entity);
                }
                if (!(await CheckUniqueOfStd(entity.Sdt!)) || !(await CheckUniqueOfCccd(entity.Cccd!)))
                    return false;
                if (!String.IsNullOrEmpty(entity.HoTen))
                {
                    nhanVienExist.HoTen = entity.HoTen;
                }
                if (entity.NgaySinh != null)
                {
                    nhanVienExist.NgaySinh = entity.NgaySinh;
                }
                if (!String.IsNullOrEmpty(entity.GioiTinh))
                {
                    nhanVienExist.GioiTinh = entity.GioiTinh;
                }
                if (!String.IsNullOrEmpty(entity.DiaChi))
                {
                    nhanVienExist.DiaChi = entity.DiaChi;
                }
                if (!String.IsNullOrEmpty(entity.Sdt))
                {
                    nhanVienExist.Sdt = entity.Sdt;
                }
                if (!String.IsNullOrEmpty(entity.Cccd))
                {
                    nhanVienExist.Cccd = entity.Cccd;
                }
                if(nhanVienExist.QuanLy != null)
                {
                    nhanVienExist.QuanLy = entity.QuanLy;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(NhanVienRepository));
                return false;
            }
        }
        public override async Task<bool> CheckUniqueOfStd(string sdt)
        {
            try
            {
                var exitNhanVien = await _dbSet.FirstOrDefaultAsync(x => x.Sdt == sdt);
                if (exitNhanVien == null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} CheckSDT method error", typeof(NhanVienRepository));
                return false;
            }
        }

        public override async Task<bool> CheckUniqueOfCccd(string cccd)
        {
            try
            {
                var exitNhanVien = await _dbSet.FirstOrDefaultAsync(x => x.Cccd == cccd);
                if (exitNhanVien == null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} CheckCCCD method error", typeof(NhanVienRepository));
                return false;
            }
        }
        public async Task<bool> CreateANhanVienWithHashPassword(NhanVien nhanVien, string password)
        {
            try
            {
                if (!_helper.CheckPassword(password))
                {
                    throw new Exception("passwork is Invalid");
                }
                //Hash Password
                var secretKey = _configuration["HashSecretKey:Value"];
                var hashPassword = _helper.HashPasswordToBytes(password, secretKey!);
                // Replace hashPassword
                var newNhanVien = nhanVien;
                newNhanVien.MatKhau = hashPassword; 
                // Add newNhanVien into database
                return await AddAsync(newNhanVien);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} create nhanvien with hash method error", typeof(NhanVienRepository));
                return false;
            }
        }

        public async Task<bool> UpdateNhanVienWithHashPassword(int id, NhanVien nhanVien, string password)
        {
            try
            {
                if (!_helper.CheckPassword(password))
                {
                    throw new Exception("passwork is Invalid");
                }
                //Hash Password
                var secretKey = _configuration["HashSecretKey:Value"];
                var hashPassword = _helper.HashPasswordToBytes(password, secretKey!);
                // Replace hashPassword
                var newNhanVien = nhanVien;
                newNhanVien.MatKhau = hashPassword;
                // Add newNhanVien into database
                return await UpdateAsync(id, newNhanVien);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update nhanvien with hash method error", typeof(NhanVienRepository));
                return false;
            }
        }

        public async Task<NhanVien?> GetBySdtAsync(string sdt)
        {
            try
            {
                var existNhanVien = await _dbSet.FirstOrDefaultAsync(x => x.Sdt == sdt);
                if (existNhanVien == null)
                    return null;
                return existNhanVien;

            } catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} uget by Sdt method error", typeof(NhanVienRepository));
                return null;
            }
        }
    }
}
