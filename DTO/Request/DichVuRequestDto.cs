using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Request
{
    public class DichVuRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string TenDV { get; set; } = null!;

        [Required]
        public double Gia { get; set; }

        [Required]
        public bool TrangThai { get; set; }
    }
}
