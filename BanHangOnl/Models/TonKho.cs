﻿using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class TonKho
{
    public int Idtk { get; set; }

    public int? Idctpn { get; set; }

    public double? SoLuong { get; set; }

    public DateTime? NgayNhap { get; set; }

    public virtual ChiTietPhieuNhap? IdctpnNavigation { get; set; }
}
