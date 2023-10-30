using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementAPI.DTO.Respone
{
    public class HoaDonResponeDto
    {
        public int MaHD { get; set; }
        public string? HoTenKhachHang { get; set; }
        public string? HoTenNhanVien { get; set; }
        public string? TenPhong { get; set; }
        public DateTime? NgayCheckIn { get; set;}
        public DateTime? NgayCheckOut { get; set; }
        public double TriGiaDonHang { get; set; }
    }
}
