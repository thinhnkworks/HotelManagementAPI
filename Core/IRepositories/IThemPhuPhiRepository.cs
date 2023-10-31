using HotelManagementAPI.Models;

namespace HotelManagementAPI.Core.IRepositories
{
    public interface IThemPhuPhiRepository: IGenericRepository<SuKienThemPhuPhi>
    {
        public Task<double> TongTienPhuPhi(SuKienThemPhuPhi themPhuPhi);
    }
}
