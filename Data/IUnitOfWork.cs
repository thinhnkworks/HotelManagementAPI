using HotelManagementAPI.Core.IRepositories;

namespace HotelManagementAPI.Data
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        INhanVienRepository NhanViens { get; }
        ILoaiPhongRepository LoaiPhongs { get; }
        IPhongRepository Phongs { get; }
        IPhuPhiRepository PhuPhis { get; }
        IDichVuRepository DichVus { get;  }
        Task CompleteAsync();
    }
}
