using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class TinTuc
{
    public int Idtt { get; set; }

    public string TenTt { get; set; } = null!;

    public string ChiTiet { get; set; } = null!;

    public string Img { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public string? NguoiTao { get; set; }

    public DateTime NgaySua { get; set; }

    public string? NguoiSua { get; set; }

    public string? Alias { get; set; }

    public bool Active { get; set; }

    public int? Idntt { get; set; }

    public bool? HienThi { get; set; }

    public virtual NhomTinTuc? IdnttNavigation { get; set; }
}
