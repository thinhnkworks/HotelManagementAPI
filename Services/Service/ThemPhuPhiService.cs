using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.Models;
using HotelManagementAPI.Services.IService;
using System.Net.WebSockets;

namespace HotelManagementAPI.Services.Service
{
    public class ThemPhuPhiService : IThemPhuPhiService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ThemPhuPhiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> deleteThemPhuPhi(int id)
        {
            var skThemPhuPhi = await _unitOfWork.ThemPhuPhis.GetAsync(id);
            double Tien = 0;
            int MaSKTP = 0;
            if(skThemPhuPhi != null)
            {
                Tien = await _unitOfWork.ThemPhuPhis.TongTienPhuPhi(skThemPhuPhi);
                MaSKTP = skThemPhuPhi.MaSkdp;
            }
            var successDeleted = await _unitOfWork.ThemPhuPhis.DeleteAsync(id);
            if (successDeleted) {
                await _unitOfWork.CompleteAsync();
                var successUpdateHoaDon = await _unitOfWork.HoaDons.UpdateTienPhuPhi(MaSKTP, -Tien);
                if (successUpdateHoaDon)
                {
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<ThemPhuPhiResponeDto?> getThemPhuPhi(int id)
        {
            var skThemPhuPhi = await _unitOfWork.ThemPhuPhis.GetAsync(id);
            if(skThemPhuPhi == null)
            {
                return null;
            }
            return ConvertTo(skThemPhuPhi);
        }

        public async Task<IEnumerable<ThemPhuPhiResponeDto>> getThemPhuPhis()
        {
            return (await _unitOfWork.ThemPhuPhis.GetAllAsync()).Select(x => ConvertTo(x));
        }

        public async Task<bool> patchThemPhuPhi(int id, ThemPhuPhiRequestDto dto)
        {
            try
            {
                var skThemPhuPhi = await _unitOfWork.ThemPhuPhis.GetAsync(id);
                double TienCu = 0;
                int MaSKTP = 0;
                if (skThemPhuPhi != null)
                {
                    TienCu = await _unitOfWork.ThemPhuPhis.TongTienPhuPhi(skThemPhuPhi);
                    MaSKTP = skThemPhuPhi.MaSkdp;
                }
                var SkPhuPhi = ConvertTo(dto);
                var successInserted = await _unitOfWork.ThemPhuPhis.UpdateAsync(id, SkPhuPhi);
                if (successInserted)
                {
                    await _unitOfWork.CompleteAsync();
                    double TienMoi = await _unitOfWork.ThemPhuPhis.TongTienPhuPhi(skThemPhuPhi!);
                    var successUpdateHoaDon = await _unitOfWork.HoaDons.UpdateTienPhuPhi(MaSKTP, (-TienCu + TienMoi));
                    if (successUpdateHoaDon)
                    {
                        await _unitOfWork.CompleteAsync();
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex) {
                throw new Exception("Loi xay ra khi thuc hien ham patchThemPhuPhi");
            }
        }
        public async Task<ThemPhuPhiResponeDto?> postThemPhuPhi(ThemPhuPhiRequestDto dto)
        {
            try
            {
                var skThemPhuPhi = ConvertTo(dto);
                var successInsert = await _unitOfWork.ThemPhuPhis.AddAsync(skThemPhuPhi);
                if(successInsert)
                {
                    await _unitOfWork.CompleteAsync();
                    double Tien = await _unitOfWork.ThemPhuPhis.TongTienPhuPhi(skThemPhuPhi);
                    var successUpdateHoaDon = await _unitOfWork.HoaDons.UpdateTienPhuPhi(skThemPhuPhi.MaSkdp, Tien);
                    if (successUpdateHoaDon)
                    {
                        await _unitOfWork.CompleteAsync();
                        return ConvertTo(skThemPhuPhi);
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("Co loi xay ra khi goi ham postThemPhuPhi");
            }
        }
        private SuKienThemPhuPhi ConvertTo(ThemPhuPhiRequestDto entity)
        {
            try
            {
                return new SuKienThemPhuPhi()
                {
                   MaNv = entity.MaNV,
                   MaPp = entity.MaPP,
                   MaSkdp = entity.MaSKDP,
                   SoLuong = entity.SoLuong,
                   ThoiGian = entity.ThoiGian,
                };
            }
            catch
            {
                throw new Exception("Co loi xay ra khi convert tu ThemSuKienRequestDto sang SuKienThemPhuPhi");
            }
        }
        private ThemPhuPhiResponeDto ConvertTo(SuKienThemPhuPhi entity)
        {
            try
            {
                return new ThemPhuPhiResponeDto()
                {
                    MaNV = entity.MaNv,
                    MaPP = entity.MaPp,
                    MaSK = entity.MaSk,
                    MaSKDP = entity.MaSkdp,
                    SoLuong = entity.SoLuong,
                    ThoiGian = entity.ThoiGian,
                };
            }
            catch {
                throw new Exception("Co loi xay ra khi convert tu SuKienPhuPhi sang ThemPhuPhiResponeDto");
            }

        }
        private async Task<double> TienPhuPhi(SuKienThemPhuPhi suKienPhuPhi)
        {
            double Tien = 0;
            if(suKienPhuPhi !=  null)
            {
                Tien = await _unitOfWork.ThemPhuPhis.TongTienPhuPhi(suKienPhuPhi);
            }
            return Tien;
        }
    }
}
