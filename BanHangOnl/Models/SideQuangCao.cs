using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class SideQuangCao
{
    public int Id { get; set; }

    public string? Img { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? NguoiTao { get; set; }

    public bool? IsDefault { get; set; }

    public string? NguoiSua { get; set; }

    public bool? Active { get; set; }

    public bool? HienThi { get; set; }
}
