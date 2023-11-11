using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class DonDatHang
{
    public int Idpx { get; set; }

    public int? Idkh { get; set; }

    public int? Idnv { get; set; }

    public string? SoPx { get; set; }

    public string? SoHd { get; set; }

    public DateTime? NgayHd { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? GhiChu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; } = new List<ChiTietDonDatHang>();

    public virtual NhanVien? IdnvNavigation { get; set; }
}
