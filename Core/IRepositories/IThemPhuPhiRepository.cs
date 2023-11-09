using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IThemPhuPhiRepository: IGenericRepository<SuKienThemPhuPhi>
    {
        Task<double> TongTienPhuPhi(SuKienThemPhuPhi themPhuPhi);
        Task<IEnumerable<SuKienThemPhuPhi>> DanhSachPhuPhiTheoPhongVaMaSK(int? MaSKDatPhong, int? MaPhong);
    }
}
