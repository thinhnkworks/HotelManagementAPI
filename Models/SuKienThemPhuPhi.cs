using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("SuKienThemPhuPhi")]
public partial class SuKienThemPhuPhi
{
    [Key]
    [Column("MaSK")]
    public int MaSk { get; set; }

    [Column("MaPP")]
    public int MaPp { get; set; }

    [Column("MaNV")]
    public int MaNv { get; set; }

    [Column("MaSKDP")]
    public int MaSkdp { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ThoiGian { get; set; }

    [ForeignKey("MaNv")]
    [InverseProperty("SuKienThemPhuPhis")]
    public virtual NhanVien MaNvNavigation { get; set; } = null!;

    [ForeignKey("MaPp")]
    [InverseProperty("SuKienThemPhuPhis")]
    public virtual PhuPhi MaPpNavigation { get; set; } = null!;

    [ForeignKey("MaSkdp")]
    [InverseProperty("SuKienThemPhuPhis")]
    public virtual SuKienDatPhong MaSkdpNavigation { get; set; } = null!;
}
