using System;
using System.Collections.Generic;

namespace BanHangOnl.Models;

public partial class ImgHangHoa
{
    public int Id { get; set; }

    public int? Idhh { get; set; }

    public string? Img { get; set; }

    public bool? IsDefault { get; set; }

    public virtual HangHoa? IdhhNavigation { get; set; }
}
