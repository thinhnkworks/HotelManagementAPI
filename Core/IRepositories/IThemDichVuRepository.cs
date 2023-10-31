using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IThemDichVuRepository: IGenericRepository<SuKienSuDungDichVu>
    {
        public Task<double> TongTienDichVu(SuKienSuDungDichVu themDichVu);
    }
}
