using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Request
{
    public class ThemDichVuRequestDto
    {
        [Required]
        public int MaDV { get; set; }

        [Required]
        public int MaNV { get; set; }

        [Required]
        public int MaSKDP { get; set; }

        [Required]
        public int SoLuong { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
