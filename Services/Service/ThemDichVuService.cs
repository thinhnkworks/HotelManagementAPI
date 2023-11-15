using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.Models;
using HotelManagementAPI.Services.IService;

namespace HotelManagementAPI.Services.Service
{
    public class ThemDichVuService: IThemDichVuService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ThemDichVuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> deleteThemDichVu(int id)
        {
            var skThemDichVu = await _unitOfWork.ThemDichVus.GetAsync(id);
            double Tien = 0;
            int MaSKTP = 0;
            if (skThemDichVu != null)
            {
                Tien = await _unitOfWork.ThemDichVus.TongTienDichVu(skThemDichVu);
                MaSKTP = skThemDichVu.MaSkdp;
            }
            var successDeleted = await _unitOfWork.ThemDichVus.DeleteAsync(id);
            if (successDeleted)
            {
                await _unitOfWork.CompleteAsync();
                var successUpdateHoaDon = await _unitOfWork.HoaDons.UpdateTienDichVu(MaSKTP, -Tien);
                if (successUpdateHoaDon)
                {
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<ThemDichVuResponeDto?> getThemDichVu(int id)
        {
            var skThemDichVu = await _unitOfWork.ThemDichVus.GetAsync(id);
            if (skThemDichVu == null)
            {
                return null;
            }
            return ConvertTo(skThemDichVu);
        }

        public async Task<IEnumerable<ThemDichVuResponeDto>> getThemDichVus(int? MaSKDP)
        {
            return (await _unitOfWork.ThemDichVus.DanhSachDichVuTheoPhongVaMaSK(MaSKDP)).Select(x => ConvertTo(x));
        }

        public async Task<bool> patchThemDichVu(int id, ThemDichVuRequestDto dto)
        {
            try
            {
                var skThemDichVu = await _unitOfWork.ThemDichVus.GetAsync(id);
                double TienCu = 0;
                int MaSKTP = 0;
                if (skThemDichVu != null)
                {
                    TienCu = await _unitOfWork.ThemDichVus.TongTienDichVu(skThemDichVu);
                    MaSKTP = skThemDichVu.MaSkdp;
                }
                var SkDichVu = ConvertTo(dto);
                var successInserted = await _unitOfWork.ThemDichVus.UpdateAsync(id, SkDichVu);
                if (successInserted)
                {
                    await _unitOfWork.CompleteAsync();
                    double TienMoi = await _unitOfWork.ThemDichVus.TongTienDichVu(skThemDichVu!);
                    var successUpdateHoaDon = await _unitOfWork.HoaDons.UpdateTienDichVu(MaSKTP, (-TienCu + TienMoi));
                    if (successUpdateHoaDon)
                    {
                        await _unitOfWork.CompleteAsync();
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception("Loi xay ra khi thuc hien ham patchThemPhuPhi");
            }
        }
        public async Task<ThemDichVuResponeDto?> postThemDichVu(ThemDichVuRequestDto dto)
        {
            try
            {
                var skThemDichVu = ConvertTo(dto);
                var successInsert = await _unitOfWork.ThemDichVus.AddAsync(skThemDichVu);
                if (successInsert)
                {
                    await _unitOfWork.CompleteAsync();
                    double Tien = await _unitOfWork.ThemDichVus.TongTienDichVu(skThemDichVu);
                    var successUpdateHoaDon = await _unitOfWork.HoaDons.UpdateTienDichVu(skThemDichVu.MaSkdp, Tien);
                    if (successUpdateHoaDon)
                    {
                        await _unitOfWork.CompleteAsync();
                        skThemDichVu.MaDvNavigation = await _unitOfWork.DichVus.GetAsync(skThemDichVu.MaDv);
                        return ConvertTo(skThemDichVu);
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("Co loi xay ra khi goi ham postThemDichvu");
            }
        }
        private SuKienSuDungDichVu ConvertTo(ThemDichVuRequestDto entity)
        {
            try
            {
                return new SuKienSuDungDichVu()
                {
                    MaNv = entity.MaNV,
                    MaDv = entity.MaDV,
                    MaSkdp = entity.MaSKDP,
                    SoLuong = entity.SoLuong,
                    ThoiGian = entity.ThoiGian,
                };
            }
            catch
            {
                throw new Exception("Co loi xay ra khi convert tu ThemDichVuRequestDto sang SuKienThemDichVu");
            }
        }
        private ThemDichVuResponeDto ConvertTo(SuKienSuDungDichVu entity)
        {
            try
            {
                return new ThemDichVuResponeDto()
                {
                    MaNV = entity.MaNv,
                    MaDV = entity.MaDv,
                    MaSK = entity.MaSk,
                    MaSKDP = entity.MaSkdp,
                    SoLuong = entity.SoLuong,
                    ThoiGian = entity.ThoiGian,
                    TongTien = entity.MaDvNavigation.Gia * entity.SoLuong,
                    TenDichVu = entity.MaDvNavigation.TenDv
                };
            }
            catch
            {
                throw new Exception("Co loi xay ra khi convert tu SuKienDichVu sang ThemDichVuResponeDto");
            }

        }
    }
}
