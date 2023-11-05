using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class Ctkm
{
    public int Id { get; set; }

    public string? TenCtkm { get; set; }

    public int? MaCtkm { get; set; }

    public string? MoTa { get; set; }

    public bool? Active { get; set; }
}
