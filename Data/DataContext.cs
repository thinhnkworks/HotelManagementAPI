using System;
using System.Collections.Generic;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Data;

public partial class DataContext : DbContext
{
	private readonly IConfiguration _configuration;

	public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
		: base(options)
	{
		this._configuration = configuration;
	}

	public virtual DbSet<DichVu> DichVus { get; set; }

	public virtual DbSet<HoaDon> HoaDons { get; set; }

	public virtual DbSet<KhachHang> KhachHangs { get; set; }

	public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

	public virtual DbSet<NhanVien> NhanViens { get; set; }

	public virtual DbSet<Phong> Phongs { get; set; }

	public virtual DbSet<PhuPhi> PhuPhis { get; set; }

	public virtual DbSet<SuKienDatPhong> SuKienDatPhongs { get; set; }

	public virtual DbSet<SuKienSuDungDichVu> SuKienSuDungDichVus { get; set; }

	public virtual DbSet<SuKienThemPhuPhi> SuKienThemPhuPhis { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:Default"]);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<DichVu>(entity =>
		{
			entity.HasKey(e => e.MaDv).HasName("PK__DichVu__272586573F20DAB7");
		});

		modelBuilder.Entity<HoaDon>(entity =>
		{
			entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E0D5BC74B4");

			entity.HasOne(d => d.MaSkdpNavigation).WithMany(p => p.HoaDons)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__HoaDon__MaSKDP__2FCF1A8A");
		});

		modelBuilder.Entity<KhachHang>(entity =>
		{
			entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E74048460");

			entity.Property(e => e.Cccd).IsFixedLength();
			entity.Property(e => e.Sdt).IsFixedLength();
		});

		modelBuilder.Entity<LoaiPhong>(entity =>
		{
			entity.HasKey(e => e.MaLoaiPhong).HasName("PK__LoaiPhon__23021217521D7D8D");
		});

		modelBuilder.Entity<NhanVien>(entity =>
		{
			entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A86923A26");

			entity.Property(e => e.Cccd).IsFixedLength();
			entity.Property(e => e.MatKhau).IsFixedLength();
			entity.Property(e => e.Sdt).IsFixedLength();
		});

		modelBuilder.Entity<Phong>(entity =>
		{
			entity.HasKey(e => e.MaPhong).HasName("PK__Phong__20BD5E5B9A0FBA7B");

			entity.HasOne(d => d.MaLoaiPhongNavigation).WithMany(p => p.Phongs).HasConstraintName("FK__Phong__MaLoaiPho__2BFE89A6");
		});

		modelBuilder.Entity<PhuPhi>(entity =>
		{
			entity.HasKey(e => e.MaPp).HasName("PK__PhuPhi__2725E7F22D5A6030");
		});

		modelBuilder.Entity<SuKienDatPhong>(entity =>
		{
			entity.HasKey(e => e.MaSk).HasName("PK__SuKienDa__272508118A834AF5");

			entity.Property(e => e.NgayNhanPhong).HasDefaultValueSql("(getdate())");
			entity.Property(e => e.NgayTraPhong).HasDefaultValueSql("(getdate())");

			entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.SuKienDatPhongs)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienDatP__MaKH__2DE6D218");

			entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.SuKienDatPhongs)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienDatP__MaNV__2EDAF651");

			entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.SuKienDatPhongs)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienDat__MaPho__2CF2ADDF");
		});

		modelBuilder.Entity<SuKienSuDungDichVu>(entity =>
		{
			entity.HasKey(e => e.MaSk).HasName("PK__SuKienSu__27250811FE11BBB3");

			entity.Property(e => e.ThoiGian).HasDefaultValueSql("(getdate())");

			entity.HasOne(d => d.MaDvNavigation).WithMany(p => p.SuKienSuDungDichVus)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienSuDu__MaDV__3A4CA8FD");

			entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.SuKienSuDungDichVus)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienSuDu__MaNV__3B40CD36");

			entity.HasOne(d => d.MaSkdpNavigation).WithMany(p => p.SuKienSuDungDichVus)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienSuD__MaSKD__3C34F16F");
		});

		modelBuilder.Entity<SuKienThemPhuPhi>(entity =>
		{
			entity.HasKey(e => e.MaSk).HasName("PK__SuKienTh__272508115C19C1A1");

			entity.Property(e => e.ThoiGian).HasDefaultValueSql("(getdate())");

			entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.SuKienThemPhuPhis)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienThem__MaNV__40058253");

			entity.HasOne(d => d.MaPpNavigation).WithMany(p => p.SuKienThemPhuPhis)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienThem__MaPP__3D2915A8");

			entity.HasOne(d => d.MaSkdpNavigation).WithMany(p => p.SuKienThemPhuPhis)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__SuKienThe__MaSKD__3F115E1A");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
