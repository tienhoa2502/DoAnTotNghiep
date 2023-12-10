using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class Voucher
{
    public int Id { get; set; }

    public string? MaVoucher { get; set; }

    public string? TenVoucher { get; set; }

    public bool? Active { get; set; }

    public bool? HienThi { get; set; }

    public int ApDung { get; set; }

    public DateTime? NgayBd { get; set; }

    public DateTime? NgayKt { get; set; }
}
