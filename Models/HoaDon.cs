using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Models;

[Table("HoaDon")]
public partial class HoaDon
{
    [Key]
    [Column("MaHD")]
    public int MaHd { get; set; }

    [Column("MaSKDP")]
    public int MaSkdp { get; set; }

    [Column("TriGiaHD")]
    public double TriGiaHd { get; set; }

    public bool DaThanhToan { get; set; }

    [ForeignKey("MaSkdp")]
    [InverseProperty("HoaDons")]
    public virtual SuKienDatPhong MaSkdpNavigation { get; set; } = null!;
}
