using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class Size
{
    public int Idsize { get; set; }

    public string? Size1 { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();
}
