using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;

namespace HotelManagementAPI.Services.IService
{
    public interface IDatPhongService
    {
        Task<IEnumerable<DatPhongResponeDto>> getDatPhongs();
        Task<DatPhongResponeDto?> getDatPhong(int id);
        Task<bool> deleteDatPhong(int id);
        Task<DatPhongResponeDto?> postDatPhong(DatPhongRequestDto dto);
        Task<bool> patchDatPhong(int id, DatPhongRequestDto dto);
    }
}
