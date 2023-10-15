using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Request
{
    public class PhuPhiRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string TenPP { get; set; } = null!;

        [Required]
        public double Gia { get; set; }
    }
}
