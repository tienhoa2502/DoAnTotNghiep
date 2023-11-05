﻿using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class NhaCungCap
{
    public int Idncc { get; set; }

    public string? MaNcc { get; set; }

    public string? TenNcc { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string? Mail { get; set; }

    public string? GhiChu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();
}
