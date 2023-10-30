using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IDatPhongRepository: IGenericRepository<SuKienDatPhong>
    {
        Task<Double> TienPhong(int MaSK);
    }
}
