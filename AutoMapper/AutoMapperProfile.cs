using AutoMapper;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.Models;

namespace HotelManagementAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<KhachHang, KhachHangResponeDto>();
            CreateMap<KhachHangRequestUpdateDto, KhachHang>();
            CreateMap<KhachHangRequestPostDto, KhachHang>();
        }

    }
}
