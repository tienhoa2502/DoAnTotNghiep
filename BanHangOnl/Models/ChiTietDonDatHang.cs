﻿using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class ChiTietDonDatHang
{
    public int Idctpx { get; set; }

    public int? Idpx { get; set; }

    public int? Idctpn { get; set; }

    public int? Idhh { get; set; }

    public double? SoLuong { get; set; }

    public double? Gia { get; set; }

    public bool? Active { get; set; }

    public virtual ChiTietPhieuNhap? IdctpnNavigation { get; set; }

    public virtual HangHoa? IdhhNavigation { get; set; }

    public virtual DonDatHang? IdpxNavigation { get; set; }
}
