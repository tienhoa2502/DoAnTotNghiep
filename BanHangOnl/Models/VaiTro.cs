using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class VaiTro
{
    public int Idvt { get; set; }

    public string? MaVt { get; set; }

    public string? TenVt { get; set; }

    public bool? NhanVien { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
