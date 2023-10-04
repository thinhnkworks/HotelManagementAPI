using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Request
{
    public class PhongRequestDto
    {
        [Required]
        public int? MaLoaiPhong { get; set; }

        [Required]
        [Range(0, 3)]
        public int TrangThai { get; set; } = 0;
    }
}
