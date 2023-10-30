using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IPhongRepository : IGenericRepository<Phong>
    {
        Task<double> tienPhong(int id);
        Task<bool> DoiTrangThai(int id, int trangthai);
    }
}
