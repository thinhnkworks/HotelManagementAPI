using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("KhachHang")]
[Index("Cccd", Name = "UQ__KhachHan__A955A0AA7B36E775", IsUnique = true)]
[Index("Sdt", Name = "UQ__KhachHan__CA1930A5B6151C3A", IsUnique = true)]
public partial class KhachHang
{
    [Key]
    [Column("MaKH")]
    public int MaKh { get; set; }

    [StringLength(100)]
    public string HoTen { get; set; } = null!;

    public int? SoLanNghi { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgaySinh { get; set; }

    [StringLength(3)]
    public string? GioiTinh { get; set; }

    [StringLength(100)]
    public string? DiaChi { get; set; }

    [Column("SDT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    [Column("CCCD")]
    [StringLength(12)]
    [Unicode(false)]
    public string? Cccd { get; set; }

    public bool XepHang { get; set; }

    [InverseProperty("MaKhNavigation")]
    public virtual ICollection<SuKienDatPhong> SuKienDatPhongs { get; set; } = new List<SuKienDatPhong>();
}
