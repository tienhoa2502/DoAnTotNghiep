using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class ChiTietPhieuNhap
{
    public int Idctpn { get; set; }

    public int? Idpn { get; set; }

    public int? Idhh { get; set; }

    public double? SoLuong { get; set; }

    public double? Gia { get; set; }

    public DateTime? Nsx { get; set; }

    public DateTime? Hsd { get; set; }

    public int? Idsize { get; set; }

    public int? SoLuongXuat { get; set; }

    public bool? Active { get; set; }

    public int? Idmau { get; set; }

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    public virtual HangHoa? IdhhNavigation { get; set; }

    public virtual Mau? IdmauNavigation { get; set; }

    public virtual PhieuNhap? IdpnNavigation { get; set; }

    public virtual Size? IdsizeNavigation { get; set; }
}
