using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Services.IServices
{
    public interface IHoaDonService
    {
        Task<IEnumerable<HoaDonResponeDto>> getHoaDons();
        Task<HoaDonResponeDto?> getHoaDon(int id, bool? check);
        Task<bool> deleteHoaDon(int id);
        Task<bool> postHoaDon(HoaDonRequestDto dto);
        Task<bool> patchHoaDon(int id, HoaDonRequestDto dto);
    }
}