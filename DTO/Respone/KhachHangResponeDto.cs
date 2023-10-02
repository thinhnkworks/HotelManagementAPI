using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Respone
{
    public class KhachHangResponeDto
    {
        [Required]
        public int MaKh { get; set; }

        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; } = null!;

        [Required]
        public int? SoLanNghi { get; set; }

        [Required]
        public DateTime? NgaySinh { get; set; }

        [MaxLength(3)]
        [Required]
        public string? GioiTinh { get; set; }

        [Required]
        [MaxLength(100)]
        public string? DiaChi { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[0-9]*$")]
        public string? Sdt { get; set; }

        [MaxLength(12)]
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public string? Cccd { get; set; }

        [Required]
        public bool XepHang { get; set; }
    }
}
