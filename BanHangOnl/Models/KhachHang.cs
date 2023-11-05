using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class KhachHang
{
    public int Idkh { get; set; }

    public string? TenKh { get; set; }

    public decimal? Phone { get; set; }

    public string? Email { get; set; }

    public int? MaKh { get; set; }

    public string? DiaChi { get; set; }
}
