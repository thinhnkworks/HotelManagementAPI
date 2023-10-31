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

        [Required]
        public int SoLuong { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
