using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface ICustomerRepository: IGenericRepository<KhachHang>
    {
        Task<KhachHang?> GetByCCCDAsync(string cccd);
    }
}
