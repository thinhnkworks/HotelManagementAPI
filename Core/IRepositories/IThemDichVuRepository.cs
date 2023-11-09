using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IThemDichVuRepository: IGenericRepository<SuKienSuDungDichVu>
    {
        Task<double> TongTienDichVu(SuKienSuDungDichVu themDichVu);
        Task<IEnumerable<SuKienSuDungDichVu>> DanhSachDichVuTheoPhongVaMaSK(int? MaSKDatPhong, int? MaPhong);
    }
}
