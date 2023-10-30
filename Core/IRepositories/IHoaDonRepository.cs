using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IHoaDonRepository: IGenericRepository<HoaDon>
    {
        Task UpdateTien(int id, double soTien);
    }
}
