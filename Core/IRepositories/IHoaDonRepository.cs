using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IHoaDonRepository: IGenericRepository<HoaDon>
    {
        Task UpdateTien(int id, double soTien);
        Task<bool> UpdateTienDichVu(int MaSK, double Tien);
        Task<bool> UpdateTienPhuPhi(int MaSK, double Tien);
    }
}
