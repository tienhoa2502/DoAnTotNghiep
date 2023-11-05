using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class DonViTinh
{
    public int Iddvt { get; set; }

    public string? MaDvt { get; set; }

    public string? TenDvt { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
}
