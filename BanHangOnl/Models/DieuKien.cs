using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class DieuKien
{
    public int? Id { get; set; }

    public DateTime? NgayBd { get; set; }

    public DateTime? NgayKt { get; set; }

    public string? TenDk { get; set; }

    public int? MaDk { get; set; }

    public bool? Active { get; set; }
}
