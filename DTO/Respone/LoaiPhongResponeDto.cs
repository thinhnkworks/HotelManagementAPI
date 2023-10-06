﻿using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Respone
{
    public class LoaiPhongResponeDto
    {
        public int MaLoaiPhong { get; set; }

        public string TenLoaiPhong { get; set; } = null!;

        public int SoNguoiO { get; set; }

        public double Gia { get; set; }

    }
}
