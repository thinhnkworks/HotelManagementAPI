using HotelManagementAPI.Core.IRepositories;

namespace HotelManagementAPI.Data
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        INhanVienRepository NhanViens { get; }
        Task CompleteAsync();
    }
}
