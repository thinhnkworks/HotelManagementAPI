using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface INhanVienRepository: IGenericRepository<NhanVien>
    {
        Task<bool> CreateANhanVienWithHashPassword(NhanVien nhanVien, string password);
        Task<bool> UpdateNhanVienWithHashPassword(int id, NhanVien nhanVien, string password);
        Task<NhanVien?> GetBySdtAsync(string sdt);
    }
}
