using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Request
{
    public class LoaiPhongRequestDto
    {
        [MaxLength(100)]
        [Required]
        public string TenLoaiPhong { get; set; } = null!;

        [Required]
        [Range(1, 10)]
        public int SoNguoiO { get; set; }

        [Required]
        public double Gia { get; set; }
    }
}
