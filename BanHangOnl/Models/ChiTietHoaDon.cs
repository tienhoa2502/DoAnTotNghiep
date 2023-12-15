using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class ChiTietHoaDon
{
    public int Idcthd { get; set; }

    public int? Idtd { get; set; }

    public int? Idhd { get; set; }

    public int? Sl { get; set; }

    public double? DonGia { get; set; }

    public double? ThanhTien { get; set; }

    public bool? TrangThaiOrder { get; set; }

    public bool? Active { get; set; }

    public virtual HoaDon? IdhdNavigation { get; set; }
}
