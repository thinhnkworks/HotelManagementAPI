using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementAPI.DTO.Request
{
    public class ThemPhuPhiRequestDto
    {
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
