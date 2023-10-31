using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;

namespace HotelManagementAPI.Services.IService
{
    public interface IThemDichVuService
    {
        Task<IEnumerable<ThemDichVuResponeDto>> getThemDichVus();
        Task<ThemDichVuResponeDto?> getThemDichVu(int id);
        Task<bool> deleteThemDichVu(int id);
        Task<ThemDichVuResponeDto?> postThemDichVu(ThemDichVuRequestDto dto);
        Task<bool> patchThemDichVu(int id, ThemDichVuRequestDto dto);
    }
}
