﻿using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class HangHoa
{
    public int Idhh { get; set; }

    public int? Idnhh { get; set; }

    public int? Iddvt { get; set; }

    public string? MaHh { get; set; }

    public string? TenHh { get; set; }

    public bool? Active { get; set; }

    public bool? HienThi { get; set; }

    public string? Idsize { get; set; }

    public string? Idmau { get; set; }

    public double? GiaBan { get; set; }

    public double? GiaSale { get; set; }

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    public virtual DonViTinh? IddvtNavigation { get; set; }

    public virtual NhomHangHoa? IdnhhNavigation { get; set; }

    public virtual ICollection<ImgHangHoa> ImgHangHoas { get; set; } = new List<ImgHangHoa>();
}
