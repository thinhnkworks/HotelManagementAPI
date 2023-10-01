using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("SuKienDatPhong")]
public partial class SuKienDatPhong
{
    [Key]
    [Column("MaSK")]
    public int MaSk { get; set; }

    public int MaPhong { get; set; }

    [Column("MaKH")]
    public int MaKh { get; set; }

    [Column("MaNV")]
    public int MaNv { get; set; }

    public int? SoNgayO { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayNhanPhong { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTraPhong { get; set; }

    [InverseProperty("MaSkdpNavigation")]
    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    [ForeignKey("MaKh")]
    [InverseProperty("SuKienDatPhongs")]
    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    [ForeignKey("MaNv")]
    [InverseProperty("SuKienDatPhongs")]
    public virtual NhanVien MaNvNavigation { get; set; } = null!;

    [ForeignKey("MaPhong")]
    [InverseProperty("SuKienDatPhongs")]
    public virtual Phong MaPhongNavigation { get; set; } = null!;

    [InverseProperty("MaSkdpNavigation")]
    public virtual ICollection<SuKienSuDungDichVu> SuKienSuDungDichVus { get; set; } = new List<SuKienSuDungDichVu>();

    [InverseProperty("MaSkdpNavigation")]
    public virtual ICollection<SuKienThemPhuPhi> SuKienThemPhuPhis { get; set; } = new List<SuKienThemPhuPhi>();
}
