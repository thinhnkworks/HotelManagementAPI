using AutoMapper;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.Models;

namespace HotelManagementAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<KhachHang, KhachHangResponeDto>()
                .ForMember(des => des.MaKH, opts => opts.MapFrom(src => src.MaKh));
            CreateMap<KhachHangRequestDto, KhachHang>();
        }
    }
}
