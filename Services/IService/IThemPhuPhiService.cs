using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;

namespace HotelManagementAPI.Services.IService
{
    public interface IThemPhuPhiService
    {
        Task<IEnumerable<ThemPhuPhiResponeDto>> getThemPhuPhis();
        Task<ThemPhuPhiResponeDto?> getThemPhuPhi(int id);
        Task<bool> deleteThemPhuPhi(int id);
        Task<ThemPhuPhiResponeDto?> postThemPhuPhi(ThemPhuPhiRequestDto dto);
        Task<bool> patchThemPhuPhi(int id, ThemPhuPhiRequestDto dto);
    }
}
