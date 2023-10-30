using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Respone
{
    public class DatPhongResponeDto
    {
        [Required]
        public int MaSK { get; set; }
        [Required]
        public int MaPhong { get; set; }

        [Required]
        public int MaKH { get; set; }

        [Required]
        public int MaNV { get; set; }

        [Required]
        public int? SoNgayO { get; set; }

        [Required]
        public DateTime? NgayNhanPhong { get; set; }

        [Required]
        public DateTime? NgayTraPhong { get; set; }
    }
}
