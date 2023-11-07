using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class NhomTinTuc
{
    public int Idntt { get; set; }

    public string? TenNtt { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<TinTuc> TinTucs { get; set; } = new List<TinTuc>();
}
