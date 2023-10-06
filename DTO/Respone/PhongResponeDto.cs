using HotelManagementAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementAPI.DTO.Respone
{
    public class PhongResponeDto
    {
        public int MaPhong { get; set; }

        public int TrangThai { get; set; }

        public LoaiPhongDto? LoaiPhong { get; set; }
    }
}
