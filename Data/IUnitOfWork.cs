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
        IDatPhongRepository DatPhongs { get; } 
        IHoaDonRepository HoaDons { get; }
        IThemPhuPhiRepository ThemPhuPhis { get; }
        Task CompleteAsync();
    }
}
