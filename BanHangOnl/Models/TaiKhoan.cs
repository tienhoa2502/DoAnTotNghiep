using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class TaiKhoan
{
    public int Idtk { get; set; }

    public string? TenTk { get; set; }

    public string? Pass { get; set; }

    public bool? Active { get; set; }

    public int? Idvt { get; set; }

    public virtual VaiTro? IdvtNavigation { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
