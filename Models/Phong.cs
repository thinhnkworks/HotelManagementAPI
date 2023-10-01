using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("Phong")]
public partial class Phong
{
    [Key]
    public int MaPhong { get; set; }

    public int? MaLoaiPhong { get; set; }

    public int TrangThai { get; set; }

    [ForeignKey("MaLoaiPhong")]
    [InverseProperty("Phongs")]
    public virtual LoaiPhong? MaLoaiPhongNavigation { get; set; }

    [InverseProperty("MaPhongNavigation")]
    public virtual ICollection<SuKienDatPhong> SuKienDatPhongs { get; set; } = new List<SuKienDatPhong>();
}
