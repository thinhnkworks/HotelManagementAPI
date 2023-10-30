﻿using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.Models;
using HotelManagementAPI.Services.IServices;

namespace HotelManagementAPI.Services.Services
{
    public class HoaDonService : IHoaDonService
    {
        private IUnitOfWork _unitOfWork;

        public HoaDonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> deleteHoaDon(int id)
        {
            return await _unitOfWork.HoaDons.DeleteAsync(id);
        }

        public async Task<HoaDonResponeDto?> getHoaDon(int id, bool? check)
        {
            var hoaDon = await _unitOfWork.HoaDons.GetAsync(id);
            if(hoaDon == null)
                return null;
            var responeHoaDon = Convert(hoaDon);
            if (check.HasValue && check == true)
            {
                hoaDon.DaThanhToan = true;
                responeHoaDon.NgayCheckOut = DateTime.UtcNow;
            }
            return responeHoaDon;
        }
        public async Task<IEnumerable<HoaDonResponeDto>> getHoaDons()
        {
            var hoaDons = (await _unitOfWork.HoaDons.GetAllAsync()).Select(x => Convert(x));
            return hoaDons;
        }

        public async Task<bool> patchHoaDon(int id, HoaDonRequestDto dto)
        {
            HoaDon hoaDon = Convert(dto);
            return await _unitOfWork.HoaDons.UpdateAsync(id, hoaDon);
        }

        public async Task<bool> postHoaDon(HoaDonRequestDto dto)
        {
            var hoaDon = Convert(dto);
            var success = await _unitOfWork.HoaDons.AddAsync(hoaDon);
            if (success == false)
                return false;
            return true;
        }
        private HoaDon Convert(HoaDonRequestDto dto)
        {
            return new HoaDon()
            {
                MaSkdp = dto.MaSkdp,
                DaThanhToan = dto.DaThanhToan,
            };
        }
        private HoaDonResponeDto Convert(HoaDon hoaDon)
        {
            try
            {
                var SkDatPhong = hoaDon.MaSkdpNavigation;
                var KhachHang = SkDatPhong.MaKhNavigation.HoTen;
                var NhanVien = SkDatPhong.MaNvNavigation.HoTen;
                var CheckIn = SkDatPhong.NgayNhanPhong;
                var LoaiPhong = SkDatPhong.MaPhongNavigation.MaLoaiPhongNavigation!.TenLoaiPhong;
                return new HoaDonResponeDto
                {
                    HoTenKhachHang = KhachHang,
                    HoTenNhanVien = NhanVien,
                    MaHD = hoaDon.MaHd,
                    NgayCheckIn = CheckIn,
                    NgayCheckOut = null,
                    TenPhong = LoaiPhong,
                    TriGiaDonHang = hoaDon.TriGiaHd
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi về khi dùng lazy loading "+ ex.Message);
            }

        }
    }
}