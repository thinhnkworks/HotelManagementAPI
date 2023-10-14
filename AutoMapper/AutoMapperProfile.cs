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
            CreateMap<NhanVienRequestDto, NhanVien>()
                .ForMember(des => des.MatKhau, opts => opts.Ignore());
            CreateMap<NhanVien, NhanVienResponeDto>()
                .ForMember(des => des.MaNV, opt => opt.MapFrom(src => src.MaNv));
            CreateMap<Phong, PhongResponeDto>()
                .ForMember(des => des.LoaiPhong, opts => opts.MapFrom(src => src.MaLoaiPhongNavigation));
            CreateMap<PhongRequestDto, Phong>();
            CreateMap<Phong, PhongDto>();
            CreateMap<LoaiPhongRequestDto, LoaiPhong>();
            CreateMap<LoaiPhong, LoaiPhongDto>();
            CreateMap<LoaiPhong, LoaiPhongResponeDto>();
            CreateMap<PhuPhi, PhuPhiResponeDto>()
                .ForMember(des => des.MaPP, opt => opt.MapFrom(src => src.MaPp))
                .ForMember(des => des.TenPP, opt => opt.MapFrom(src => src.TenPp));
            CreateMap<PhuPhiRequestDto, PhuPhi>()
                .ForMember(des =>des.TenPp, opt => opt.MapFrom(src => src.TenPP));
         }
    }
}
