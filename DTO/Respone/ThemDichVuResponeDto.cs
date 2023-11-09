using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Respone
{
    public class ThemDichVuResponeDto
    {
        [Required]
        public int MaSK { get; set; }

        public Nullable<int> MaDV { get; set; }

        public Nullable<int> MaNV { get; set; }

        public Nullable<int> MaSKDP { get; set; }

        [Required]
        public int SoLuong { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
