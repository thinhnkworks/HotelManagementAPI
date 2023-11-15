using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Respone
{
    public class ThemDichVuResponeDto
    {
        [Required]
        public int MaSK { get; set; }

        public int MaDV { get; set; }

        public int MaNV { get; set; }

        public int MaSKDP { get; set; }

        public string? TenDichVu { get; set; }

        [Required]
        public int SoLuong { get; set; }

        public double TongTien { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
