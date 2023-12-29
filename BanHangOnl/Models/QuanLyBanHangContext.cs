using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Models;

public partial class QuanLyBanHangContext : DbContext
{
    public QuanLyBanHangContext()
    {
    }

    public QuanLyBanHangContext(DbContextOptions<QuanLyBanHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    public virtual DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }

    public virtual DbSet<Ctkm> Ctkms { get; set; }

    public virtual DbSet<DieuKien> DieuKiens { get; set; }

    public virtual DbSet<DonViTinh> DonViTinhs { get; set; }

    public virtual DbSet<Gium> Gia { get; set; }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<ImgHangHoa> ImgHangHoas { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<Mau> Maus { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NhomHangHoa> NhomHangHoas { get; set; }

    public virtual DbSet<NhomTinTuc> NhomTinTucs { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }

    public virtual DbSet<SideQuangCao> SideQuangCaos { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TinTuc> TinTucs { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4KER1P3\\MSSQLSERVER22;Database=QuanLyBanHang;User Id=sa;Password=123456;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.Idcthd);

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.Idcthd).HasColumnName("IDCTHD");
            entity.Property(e => e.Idhd).HasColumnName("IDHD");
            entity.Property(e => e.Idtd).HasColumnName("IDTD");
            entity.Property(e => e.Sl).HasColumnName("SL");

            entity.HasOne(d => d.IdhdNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.Idhd)
                .HasConstraintName("FK_ChiTietHoaDon_HoaDon");
        });

        modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
        {
            entity.HasKey(e => e.Idctpn);

            entity.ToTable("ChiTietPhieuNhap");

            entity.Property(e => e.Idctpn).HasColumnName("IDCTPN");
            entity.Property(e => e.Hsd)
                .HasColumnType("datetime")
                .HasColumnName("HSD");
            entity.Property(e => e.Idhh).HasColumnName("IDHH");
            entity.Property(e => e.Idmau).HasColumnName("IDMau");
            entity.Property(e => e.Idpn).HasColumnName("IDPN");
            entity.Property(e => e.Idsize).HasColumnName("IDSize");
            entity.Property(e => e.Nsx)
                .HasColumnType("datetime")
                .HasColumnName("NSX");

            entity.HasOne(d => d.IdhhNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.Idhh)
                .HasConstraintName("FK_ChiTietPhieuNhap_HangHoa");

            entity.HasOne(d => d.IdmauNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.Idmau)
                .HasConstraintName("FK_ChiTietPhieuNhap_Mau");

            entity.HasOne(d => d.IdpnNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.Idpn)
                .HasConstraintName("FK_ChiTietPhieuNhap_PhieuNhap");

            entity.HasOne(d => d.IdsizeNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.Idsize)
                .HasConstraintName("FK_ChiTietPhieuNhap_Size");
        });

        modelBuilder.Entity<ChiTietPhieuXuat>(entity =>
        {
            entity.HasKey(e => e.Idctpx);

            entity.ToTable("ChiTietPhieuXuat");

            entity.Property(e => e.Idctpx).HasColumnName("IDCTPX");
            entity.Property(e => e.Idctpn).HasColumnName("IDCTPN");
            entity.Property(e => e.Idhh).HasColumnName("IDHH");
            entity.Property(e => e.Idmau).HasColumnName("IDMau");
            entity.Property(e => e.Idpx).HasColumnName("IDPX");
            entity.Property(e => e.Idsize).HasColumnName("IDSize");

            entity.HasOne(d => d.IdctpnNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.Idctpn)
                .HasConstraintName("FK_ChiTietPhieuXuat_ChiTietPhieuNhap");

            entity.HasOne(d => d.IdhhNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.Idhh)
                .HasConstraintName("FK_ChiTietPhieuXuat_HangHoa");

            entity.HasOne(d => d.IdmauNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.Idmau)
                .HasConstraintName("FK_ChiTietPhieuXuat_Mau");

            entity.HasOne(d => d.IdpxNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.Idpx)
                .HasConstraintName("FK_ChiTietPhieuXuat_PhieuXuat");

            entity.HasOne(d => d.IdsizeNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.Idsize)
                .HasConstraintName("FK_ChiTietPhieuXuat_Size");
        });

        modelBuilder.Entity<Ctkm>(entity =>
        {
            entity.ToTable("CTKM");

            entity.Property(e => e.MaCtkm).HasColumnName("MaCTKM");
            entity.Property(e => e.TenCtkm)
                .HasMaxLength(50)
                .HasColumnName("TenCTKM");
        });

        modelBuilder.Entity<DieuKien>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DieuKien");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaDk).HasColumnName("MaDK");
            entity.Property(e => e.NgayBd)
                .HasColumnType("datetime")
                .HasColumnName("NgayBD");
            entity.Property(e => e.NgayKt)
                .HasColumnType("datetime")
                .HasColumnName("NgayKT");
            entity.Property(e => e.TenDk)
                .HasMaxLength(50)
                .HasColumnName("TenDK");
        });

        modelBuilder.Entity<DonViTinh>(entity =>
        {
            entity.HasKey(e => e.Iddvt);

            entity.ToTable("DonViTinh");

            entity.Property(e => e.Iddvt).HasColumnName("IDDVT");
            entity.Property(e => e.MaDvt)
                .HasMaxLength(50)
                .HasColumnName("MaDVT");
            entity.Property(e => e.TenDvt)
                .HasMaxLength(100)
                .HasColumnName("TenDVT");
        });

        modelBuilder.Entity<Gium>(entity =>
        {
            entity.HasKey(e => e.Idgia);

            entity.Property(e => e.Idgia).HasColumnName("IDGia");
            entity.Property(e => e.Idsanh).HasColumnName("IDSanh");
            entity.Property(e => e.Idtd).HasColumnName("IDTD");
        });

        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.Idhh);

            entity.ToTable("HangHoa");

            entity.Property(e => e.Idhh).HasColumnName("IDHH");
            entity.Property(e => e.Iddvt).HasColumnName("IDDVT");
            entity.Property(e => e.Idmau)
                .HasMaxLength(50)
                .HasColumnName("IDMau");
            entity.Property(e => e.Idnhh).HasColumnName("IDNHH");
            entity.Property(e => e.Idsize)
                .HasMaxLength(50)
                .HasColumnName("IDSize");
            entity.Property(e => e.MaHh)
                .HasMaxLength(100)
                .HasColumnName("MaHH");
            entity.Property(e => e.TenHh)
                .HasMaxLength(100)
                .HasColumnName("TenHH");

            entity.HasOne(d => d.IddvtNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.Iddvt)
                .HasConstraintName("FK_HangHoa_DonViTinh");

            entity.HasOne(d => d.IdnhhNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.Idnhh)
                .HasConstraintName("FK_HangHoa_NhomHangHoa");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.Idhd);

            entity.ToTable("HoaDon");

            entity.Property(e => e.Idhd).HasColumnName("IDHD");
            entity.Property(e => e.Idban).HasColumnName("IDBan");
            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.MaHd)
                .HasMaxLength(50)
                .HasColumnName("MaHD");
            entity.Property(e => e.Tgxuat)
                .HasColumnType("datetime")
                .HasColumnName("TGXuat");
            entity.Property(e => e.TinhTrangTt).HasColumnName("TinhTrangTT");

            entity.HasOne(d => d.IdnvNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.Idnv)
                .HasConstraintName("FK_HoaDon_NhanVien");
        });

        modelBuilder.Entity<ImgHangHoa>(entity =>
        {
            entity.ToTable("ImgHangHoa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idhh).HasColumnName("IDHH");

            entity.HasOne(d => d.IdhhNavigation).WithMany(p => p.ImgHangHoas)
                .HasForeignKey(d => d.Idhh)
                .HasConstraintName("FK_ImgHangHoa_HangHoa");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Idkh);

            entity.ToTable("KhachHang");

            entity.Property(e => e.Idkh).HasColumnName("IDKH");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Idtk).HasColumnName("IDTK");
            entity.Property(e => e.MaKh)
                .HasMaxLength(100)
                .HasColumnName("MaKH");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");

            entity.HasOne(d => d.IdtkNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.Idtk)
                .HasConstraintName("FK_KhachHang_TaiKhoan");
        });

        modelBuilder.Entity<Mau>(entity =>
        {
            entity.HasKey(e => e.Idmau);

            entity.ToTable("Mau");

            entity.Property(e => e.Idmau).HasColumnName("IDMau");
            entity.Property(e => e.Mau1)
                .HasMaxLength(50)
                .HasColumnName("Mau");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.Idncc);

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.Idncc).HasColumnName("IDNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(500);
            entity.Property(e => e.DienThoai).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(100)
                .HasColumnName("MaNCC");
            entity.Property(e => e.Mail).HasMaxLength(100);
            entity.Property(e => e.TenNcc)
                .HasMaxLength(100)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.Idnv);

            entity.ToTable("NhanVien");

            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.DiaChi).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Idnnv).HasColumnName("IDNNV");
            entity.Property(e => e.Idtk).HasColumnName("IDTK");
            entity.Property(e => e.Image).HasMaxLength(250);
            entity.Property(e => e.MaMv)
                .HasMaxLength(50)
                .HasColumnName("MaMV");
            entity.Property(e => e.QueQuan).HasMaxLength(250);
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(200);
            entity.Property(e => e.Tuoi).HasColumnType("date");

            entity.HasOne(d => d.IdtkNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.Idtk)
                .HasConstraintName("FK_NhanVien_TaiKhoan");
        });

        modelBuilder.Entity<NhomHangHoa>(entity =>
        {
            entity.HasKey(e => e.Idnhh);

            entity.ToTable("NhomHangHoa");

            entity.Property(e => e.Idnhh).HasColumnName("IDNHH");
            entity.Property(e => e.MaNhh)
                .HasMaxLength(100)
                .HasColumnName("MaNHH");
            entity.Property(e => e.TenNhh)
                .HasMaxLength(100)
                .HasColumnName("TenNHH");
        });

        modelBuilder.Entity<NhomTinTuc>(entity =>
        {
            entity.HasKey(e => e.Idntt);

            entity.ToTable("NhomTinTuc");

            entity.Property(e => e.Idntt).HasColumnName("IDNtt");
            entity.Property(e => e.TenNtt)
                .HasMaxLength(50)
                .HasColumnName("TenNTT");
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasKey(e => e.Idpn);

            entity.ToTable("PhieuNhap");

            entity.Property(e => e.Idpn).HasColumnName("IDPN");
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.Idncc).HasColumnName("IDNCC");
            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.NgayHd)
                .HasColumnType("datetime")
                .HasColumnName("NgayHD");
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            entity.Property(e => e.SoHd)
                .HasMaxLength(100)
                .HasColumnName("SoHD");
            entity.Property(e => e.SoPn)
                .HasMaxLength(100)
                .HasColumnName("SoPN");

            entity.HasOne(d => d.IdnccNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.Idncc)
                .HasConstraintName("FK_PhieuNhap_NhaCungCap");

            entity.HasOne(d => d.IdnvNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.Idnv)
                .HasConstraintName("FK_PhieuNhap_NhanVien");
        });

        modelBuilder.Entity<PhieuXuat>(entity =>
        {
            entity.HasKey(e => e.Idpx);

            entity.ToTable("PhieuXuat");

            entity.Property(e => e.Idpx).HasColumnName("IDPX");
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.Idnv).HasColumnName("IDNV");
            entity.Property(e => e.Idtk).HasColumnName("IDTK");
            entity.Property(e => e.NgayHd)
                .HasColumnType("datetime")
                .HasColumnName("NgayHD");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.SoHd)
                .HasMaxLength(100)
                .HasColumnName("SoHD");
            entity.Property(e => e.SoPx)
                .HasMaxLength(100)
                .HasColumnName("SoPX");

            entity.HasOne(d => d.IdtkNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.Idtk)
                .HasConstraintName("FK_PhieuXuat_TaiKhoan");

            entity.HasOne(d => d.IdnvNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.Idnv)
                .HasConstraintName("FK_PhieuXuat_NhanVien");

            entity.HasOne(d => d.IdtkNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.Idtk)
                .HasConstraintName("FK_PhieuXuat_TaiKhoan");
        });

        modelBuilder.Entity<SideQuangCao>(entity =>
        {
            entity.ToTable("SideQuangCao");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(50);
            entity.Property(e => e.NguoiTao).HasMaxLength(50);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Idsize);

            entity.ToTable("Size");

            entity.Property(e => e.Idsize).HasColumnName("IDSize");
            entity.Property(e => e.Size1)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Size");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Idtk);

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.Idtk).HasColumnName("IDTK");
            entity.Property(e => e.Idvt).HasColumnName("IDVT");
            entity.Property(e => e.Pass).HasMaxLength(100);
            entity.Property(e => e.TenTk)
                .HasMaxLength(100)
                .HasColumnName("TenTK");

            entity.HasOne(d => d.IdvtNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.Idvt)
                .HasConstraintName("FK_TaiKhoan_VaiTro");
        });

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasKey(e => e.Idtt);

            entity.ToTable("TinTuc");

            entity.Property(e => e.Idtt).HasColumnName("IDTT");
            entity.Property(e => e.Idntt).HasColumnName("IDNtt");
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(50);
            entity.Property(e => e.NguoiTao).HasMaxLength(50);
            entity.Property(e => e.TenTt)
                .HasMaxLength(150)
                .HasColumnName("TenTT");

            entity.HasOne(d => d.IdnttNavigation).WithMany(p => p.TinTucs)
                .HasForeignKey(d => d.Idntt)
                .HasConstraintName("FK_TinTuc_NhomTinTuc");
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.Idvt);

            entity.ToTable("VaiTro");

            entity.Property(e => e.Idvt).HasColumnName("IDVT");
            entity.Property(e => e.MaVt)
                .HasMaxLength(50)
                .HasColumnName("MaVT");
            entity.Property(e => e.TenVt)
                .HasMaxLength(200)
                .HasColumnName("TenVT");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.ToTable("Voucher");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaVoucher)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.NgayBd)
                .HasColumnType("datetime")
                .HasColumnName("NgayBD");
            entity.Property(e => e.NgayKt)
                .HasColumnType("datetime")
                .HasColumnName("NgayKT");
            entity.Property(e => e.TenVoucher).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
