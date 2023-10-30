using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.Models;
using HotelManagementAPI.Services.IService;

namespace HotelManagementAPI.Services.Service
{
    public class DatPhongService : IDatPhongService
    {
        private IUnitOfWork _unitOfWork;

        public DatPhongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> deleteDatPhong(int id)
        {
           var success = await _unitOfWork.DatPhongs.DeleteAsync(id);
           if(success == true)
            {
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false; ;
        }

        public async Task<DatPhongResponeDto?> getDatPhong(int id)
        {
            var datPhong = await _unitOfWork.DatPhongs.GetAsync(id);
            if (datPhong == null)
                return null;
            var responeHoaDon = Convert(datPhong);
            return responeHoaDon;
        }

        public async Task<IEnumerable<DatPhongResponeDto>> getDatPhongs()
        {
            var datPhongs = (await _unitOfWork.DatPhongs.GetAllAsync()).Select(x => Convert(x));
            return datPhongs;
        }

        public async Task<bool> patchDatPhong(int id, DatPhongRequestDto dto)
        {
            SuKienDatPhong datPhong = Convert(dto);

            var success =  await _unitOfWork.DatPhongs.UpdateAsync(id, datPhong);
            if(success == true)
            {
                await _unitOfWork.CompleteAsync();
                double tienTr = await _unitOfWork.DatPhongs.TienPhong(id);
                double tienSau = await _unitOfWork.Phongs.tienPhong(datPhong.MaPhong);
                if (tienTr != tienSau)
                {
                    await _unitOfWork.HoaDons.UpdateTien(id, (-tienTr + tienSau));
                    await _unitOfWork.CompleteAsync();
                }
                return true;
            }
            return false;
        }

       

        public async Task<DatPhongResponeDto?> postDatPhong(DatPhongRequestDto dto)
        {
            var datPhong = Convert(dto);
            int tinhTrangPhong = (await _unitOfWork.Phongs.GetAsync(datPhong.MaPhong))!.TrangThai;
            if (tinhTrangPhong != 0)
                return null;
            var success = await _unitOfWork.DatPhongs.AddAsync(datPhong);
            if (success == true)
            {
                await _unitOfWork.CompleteAsync();
                double tienPhong = await _unitOfWork.DatPhongs.TienPhong(datPhong.MaSk);
                double tienPhaiTra = (double)(datPhong.SoNgayO != null ? datPhong.SoNgayO * tienPhong : 0);
                var successHoaDon = await _unitOfWork.HoaDons.AddAsync(new HoaDon()
                {
                    MaSkdp = datPhong.MaSk,
                    DaThanhToan = false,
                    TriGiaHd = tienPhaiTra
                });
                var successThayDoi = await _unitOfWork.Phongs.DoiTrangThai(datPhong.MaPhong, 1);
                var successKhachHang = await _unitOfWork.Customers.TangNgayNghi(datPhong.MaKh);
                if(successHoaDon == true && successThayDoi == true && successKhachHang)
                {
                    await _unitOfWork.CompleteAsync();
                    return Convert(datPhong);
                }
            }     
            return null;
        }
        private DatPhongResponeDto Convert(SuKienDatPhong datPhong)
        {
            try
            {
                return new DatPhongResponeDto()
                {
                    MaKH = datPhong.MaKh,
                    MaNV = datPhong.MaNv,
                    MaPhong = datPhong.MaPhong,
                    MaSK = datPhong.MaSk,
                    NgayNhanPhong = datPhong.NgayNhanPhong,
                    NgayTraPhong = datPhong.NgayTraPhong,
                    SoNgayO = datPhong.SoNgayO,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi về khi dùng lazy loading " + ex.Message);
            }

        }
        private SuKienDatPhong Convert(DatPhongRequestDto datPhong)
        {
            try
            {
                return new SuKienDatPhong()
                {
                    MaKh = datPhong.MaKH,
                    MaNv = datPhong.MaNV,
                    MaPhong = datPhong.MaPhong,
                    NgayNhanPhong = datPhong.NgayNhanPhong,
                    NgayTraPhong = datPhong.NgayTraPhong,
                    SoNgayO = datPhong.SoNgayO,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi về khi dùng lazy loading " + ex.Message);
            }

        }
    }
}
