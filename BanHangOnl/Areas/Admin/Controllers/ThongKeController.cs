using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/ThongKe")]
        public IActionResult Index()
        {


            return View("BaoCaoLoiNhuan");
        }
        [HttpPost("/baoCaoLoiNhuan")]
        public async Task<dynamic> baoCaoLoiNhuan(string TuNgay, string DenNgay)
        {
            DateTime tuNgay = DateTime.ParseExact(TuNgay, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime denNgay = DateTime.ParseExact(DenNgay, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //var doanhThu = await context.HoaDons
            //  .Where(x => x.Tgxuat.Value.Date >= tuNgay.Date && x.Tgxuat.Value.Date <= denNgay.Date)
            //  .OrderBy(x => x.Tgxuat)
            //  .GroupBy(x => x.Tgxuat.Value.Date)
            //  .Select(x => new
            //  {
            //      label = x.Key,
            //      doanhthu = x.Sum(x => x.TongTien)
            //  })
            //  .ToListAsync();
            var doanhThu = context.PhieuXuats
              .Include(x => x.ChiTietPhieuXuats)
              .ThenInclude(x => x.IdctpnNavigation)
              .Where(x => x.NgayTao.Value.Date >= tuNgay.Date && x.NgayTao.Value.Date <= denNgay.Date)
              .ToList()
              .OrderBy(x => x.NgayTao)
              .GroupBy(x => x.NgayTao.Value.Date)
              .Select(x => new
              {
                  label = x.Key,
                  doanhthu = tongPhieuNhap(x.ToList()),
              })
              .ToList();
            var giaVon = context.PhieuXuats
              .Include(x => x.ChiTietPhieuXuats)
              .ThenInclude(x => x.IdctpnNavigation)
              .Where(x => x.NgayTao.Value.Date >= tuNgay.Date && x.NgayTao.Value.Date <= denNgay.Date)
              .ToList()
              .OrderBy(x => x.NgayTao)
              .GroupBy(x => x.NgayTao.Value.Date)
              .Select(x => new
              {
                  label = x.Key,
                  doanhthu = GiaVon(x.ToList()),
              })
              .ToList();
            var listLoiNhuan = new List<dynamic>();
            foreach (var d in doanhThu)
            {
                var data = new
                {
                    Ngay = d.label,
                    DoanhThu = (double)d.doanhthu,
                    GiaVon = (double)0,
                };
                listLoiNhuan.Add(data);
            }
            foreach (var d in giaVon)
            {
                var data = new
                {
                    Ngay = d.label,
                    DoanhThu = (double)0,
                    GiaVon = (double)d.doanhthu,
                };
                listLoiNhuan.Add(data);
            }
            List<LoiNhuan> newlist = listLoiNhuan.GroupBy(x => x.Ngay)
            .Select(x => new LoiNhuan()
            {
                Ngay = x.Key,
                DoanhThu = (double)x.Sum(item => (double)item.DoanhThu),
                GiaVon = (double)x.Sum(item => (double)item.GiaVon)
            }).ToList();
            return new
            {
                doanhThu = doanhThu,
                giaVon = giaVon,
                listLoiNhuan = newlist,
            };
        }
        public class LoiNhuan
        {
            public DateTime? Ngay { get; set; }
            public double? DoanhThu { get; set; }
            public double? GiaVon { get; set; }
        }
        public double tongPhieuNhap(List<PhieuXuat> phieuNhaps)
        {
            double tong = (double)phieuNhaps.Sum(x => x.ChiTietPhieuXuats.Sum(ct => ct.Gia));
            return tong;
        }
        public double GiaVon(List<PhieuXuat> phieuNhaps)
        {
            double tong = (double)phieuNhaps.Sum(x => x.ChiTietPhieuXuats.Sum(ct => ct.IdctpnNavigation.Gia ?? 0));
            return tong;
        }
        public double tongChiTietPhieuNhap(List<ChiTietPhieuNhap> chiTietPhieuNhaps)
        {
            double tong = (double)chiTietPhieuNhaps.Sum(ct => ct.Gia);
            return tong;
        }
    }
}
