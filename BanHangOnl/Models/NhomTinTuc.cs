using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class NhomTinTuc
{
    public int Id { get; set; }

    public string? TenNtt { get; set; }

    public bool? Active { get; set; }
}
