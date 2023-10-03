using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface ICustomerRepository: IGenericRepository<KhachHang>
    {
        Task<bool> CheckUniqueOfStd(string sdt);
        Task<bool> CheckUniqueOfCccd(string cccd);
        Task<KhachHang?> GetByCCCDAsync(string cccd);
    }
}
