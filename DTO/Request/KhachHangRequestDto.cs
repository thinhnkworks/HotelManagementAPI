using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Request
{
    public class KhachHangRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; } = null!;

        [Required]
        public DateTime? NgaySinh { get; set; }

        [MaxLength(3)]
        public string? GioiTinh { get; set; }

        [MaxLength(100)]
        public string? DiaChi { get; set; }

        [MinLength(10)]
        [RegularExpression(@"^[0-9]*$")]
        public string? Sdt { get; set; }

        [MinLength(12)]
        [RegularExpression(@"^[0-9]*$")]
        public string? Cccd { get; set; }
    }
}
