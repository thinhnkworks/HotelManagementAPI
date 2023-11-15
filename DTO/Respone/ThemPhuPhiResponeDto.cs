using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Respone
{
    public class ThemPhuPhiResponeDto
    {
        [Required]
        public int MaSK { get; set; }
        [Required]
        public int MaPP { get; set; }

        [Required]
        public int MaNV { get; set; }

        [Required]
        public int MaSKDP { get; set; }

        public string? TenPhuPhi { get; set; }

        [Required]
        public int SoLuong { get; set; }
        public double TongTien { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
