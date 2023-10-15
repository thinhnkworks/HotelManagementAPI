using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Respone
{
    public class PhuPhiResponeDto
    {
        [Required]
        public int MaPP { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenPP { get; set; } = null!;

        [Required]
        public double Gia { get; set; }
    }
}
