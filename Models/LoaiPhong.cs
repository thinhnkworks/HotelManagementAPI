using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("LoaiPhong")]
public partial class LoaiPhong
{
    [Key]
    public int MaLoaiPhong { get; set; }

    [StringLength(100)]
    public string TenLoaiPhong { get; set; } = null!;

    public int SoNguoiO { get; set; }

    public double Gia { get; set; }

    [InverseProperty("MaLoaiPhongNavigation")]
    public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
}
