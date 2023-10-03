using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Sieve.Attributes;

namespace HotelManagementAPI.Models;

[Table("NhanVien")]
[Index("Cccd", Name = "UQ__NhanVien__A955A0AA12BF42E5", IsUnique = true)]
[Index("Sdt", Name = "UQ__NhanVien__CA1930A5A0373CD9", IsUnique = true)]
public partial class NhanVien
{
    [Key]
    [Column("MaNV")]
    [Sieve(CanSort = true)]
    public int MaNv { get; set; }

    [StringLength(100)]
    [Sieve(CanFilter = true)]
    public string HoTen { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? NgaySinh { get; set; }

    [StringLength(3)]
    public string? GioiTinh { get; set; }

    [StringLength(100)]
    public string? DiaChi { get; set; }

    [Column("SDT")]
    [StringLength(10)]
    [Unicode(false)]
    [Sieve(CanFilter = true, CanSort = true)]
    public string? Sdt { get; set; }

    [Column("CCCD")]
    [StringLength(12)]
    [Unicode(false)]
    [Sieve(CanFilter = true, CanSort = true)]
    public string? Cccd { get; set; }

    public bool? QuanLy { get; set; }

    [MaxLength(32)]
    public byte[] MatKhau { get; set; } = null!;

    [InverseProperty("MaNvNavigation")]
    public virtual ICollection<SuKienDatPhong> SuKienDatPhongs { get; set; } = new List<SuKienDatPhong>();

    [InverseProperty("MaNvNavigation")]
    public virtual ICollection<SuKienSuDungDichVu> SuKienSuDungDichVus { get; set; } = new List<SuKienSuDungDichVu>();

    [InverseProperty("MaNvNavigation")]
    public virtual ICollection<SuKienThemPhuPhi> SuKienThemPhuPhis { get; set; } = new List<SuKienThemPhuPhi>();
}
