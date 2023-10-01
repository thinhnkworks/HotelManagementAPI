using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("SuKienSuDungDichVu")]
public partial class SuKienSuDungDichVu
{
    [Key]
    [Column("MaSK")]
    public int MaSk { get; set; }

    [Column("MaDV")]
    public int MaDv { get; set; }

    [Column("MaNV")]
    public int MaNv { get; set; }

    [Column("MaSKDP")]
    public int MaSkdp { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ThoiGian { get; set; }

    [ForeignKey("MaDv")]
    [InverseProperty("SuKienSuDungDichVus")]
    public virtual DichVu MaDvNavigation { get; set; } = null!;

    [ForeignKey("MaNv")]
    [InverseProperty("SuKienSuDungDichVus")]
    public virtual NhanVien MaNvNavigation { get; set; } = null!;

    [ForeignKey("MaSkdp")]
    [InverseProperty("SuKienSuDungDichVus")]
    public virtual SuKienDatPhong MaSkdpNavigation { get; set; } = null!;
}
