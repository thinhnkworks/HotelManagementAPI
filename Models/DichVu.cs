using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("DichVu")]
public partial class DichVu
{
    [Key]
    [Column("MaDV")]
    public int MaDv { get; set; }

    [Column("TenDV")]
    [StringLength(100)]
    public string TenDv { get; set; } = null!;

    public double Gia { get; set; }

    public bool TrangThai { get; set; }

    [InverseProperty("MaDvNavigation")]
    public virtual ICollection<SuKienSuDungDichVu> SuKienSuDungDichVus { get; set; } = new List<SuKienSuDungDichVu>();
}
