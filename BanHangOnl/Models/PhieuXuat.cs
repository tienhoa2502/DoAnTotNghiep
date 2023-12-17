using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class PhieuXuat
{
    public int Idpx { get; set; }

    public int? Idtk { get; set; }

    public int? Idnv { get; set; }

    public string? SoPx { get; set; }

    public string? SoHd { get; set; }

    public DateTime? NgayHd { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? GhiChu { get; set; }

    public bool? Active { get; set; }

    public bool? DonTra { get; set; }

    public bool? DaGiao { get; set; }

    public int? TyLeGiam { get; set; }

    public double? TongTien { get; set; }

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    public virtual NhanVien? IdnvNavigation { get; set; }

    public virtual TaiKhoan? IdtkNavigation { get; set; }
}
