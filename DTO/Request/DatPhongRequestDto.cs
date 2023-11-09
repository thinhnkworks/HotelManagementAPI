using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HotelManagementAPI.DTO.Request
{
    public class DatPhongRequestDto
    {
        [Required]
        public int MaPhong { get; set; }

        [Required]
        public int MaKH { get; set; }

        [Required]
        public int MaNV { get; set; }

        [Required]
        public int SoNgayO { get; set; }

        [Required]
        public DateOnly NgayNhanPhong { get; set; }

        [Required]
        public DateOnly NgayTraPhong { get; set; }
    }
}
