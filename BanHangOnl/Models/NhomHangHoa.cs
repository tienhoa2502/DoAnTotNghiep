using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class NhomHangHoa
{
    public int Idnhh { get; set; }

    public string? MaNhh { get; set; }

    public string? TenNhh { get; set; }

    public bool? Active { get; set; }

    public int? Levels { get; set; }

    public bool? HienThi { get; set; }

    public int? Thuoc { get; set; }

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
}
