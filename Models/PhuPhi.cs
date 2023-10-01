using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("PhuPhi")]
public partial class PhuPhi
{
    [Key]
    [Column("MaPP")]
    public int MaPp { get; set; }

    [Column("TenPP")]
    [StringLength(100)]
    public string TenPp { get; set; } = null!;

    public double Gia { get; set; }

    [InverseProperty("MaPpNavigation")]
    public virtual ICollection<SuKienThemPhuPhi> SuKienThemPhuPhis { get; set; } = new List<SuKienThemPhuPhi>();
}
