using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementAPI.DTO.Request
{
    public class HoaDonRequestDto
    {
        [Required]
        public int MaSkdp { get; set; }
        [Required]
        public bool DaThanhToan { get; set; }
    }
}
