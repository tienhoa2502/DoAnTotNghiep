using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class NhanVien
{
    public int Idnv { get; set; }

    public string? MaMv { get; set; }

    public string? Ten { get; set; }

    public bool? GioiTinh { get; set; }

    public string? Image { get; set; }

    public DateTime? Tuoi { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public bool? Active { get; set; }

    public string? QueQuan { get; set; }

    public string? Email { get; set; }

    public int? Idtk { get; set; }

    public int? Idnnv { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual TaiKhoan? IdtkNavigation { get; set; }

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
}
