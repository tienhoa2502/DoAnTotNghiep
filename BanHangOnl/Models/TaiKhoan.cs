using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnl.Models;

public partial class TaiKhoan
{
    public int Idtk { get; set; }

    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? TenTk { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string? Pass { get; set; }

    public bool? Active { get; set; }

    public int? Idvt { get; set; }

    public virtual VaiTro? IdvtNavigation { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();

}
