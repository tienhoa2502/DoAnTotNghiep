using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class KhachHang
{
    public int Idkh { get; set; }

    public string? TenKh { get; set; }

    public decimal? Phone { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public bool? Active { get; set; }

    public string? MaKh { get; set; }
}
