using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class HoaDon
{
    public int Idhd { get; set; }

    public string? MaHd { get; set; }

    public DateTime? Tgxuat { get; set; }

    public double? TongTien { get; set; }

    public bool? TinhTrang { get; set; }

    public bool? TinhTrangTt { get; set; }

    public bool? Active { get; set; }

    public int? Idban { get; set; }

    public int? Idnv { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual NhanVien? IdnvNavigation { get; set; }
}
