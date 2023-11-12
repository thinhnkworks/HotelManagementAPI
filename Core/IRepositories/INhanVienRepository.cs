using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface INhanVienRepository: IGenericRepository<NhanVien>
    {
        Task<bool> CreateANhanVienWithHashPassword(NhanVien nhanVien, string password);
        Task<bool> UpdateNhanVienWithHashPassword(int id, NhanVien nhanVien, string password);
        Task<NhanVien?> GetBySdtAsync(string sdt);
        Task<NhanVien?> GetByCCCDAsync(string CCCD);
        Task<bool> CheckUniqueOfStd(string sdt);
        Task<bool> CheckUniqueOfCccd(string cccd);
        bool CheckPassword(NhanVien nhanvien, string password);
    }
}
