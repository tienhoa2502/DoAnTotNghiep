using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TonKhoController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/TonKho")]
        public IActionResult Index()
        {
            ViewBag.NhomTinTuc = context.NhomTinTucs.Where(x => x.Active == true).ToList();

            return View("TonKho");
        }
        [HttpPost("/TonKho/BaoCaoTongHop")]
        public async Task<dynamic> getBaoCaoTongHop(int idNhomHang, int idHangHoa, string tuNgay, string denNgay)
        {
            try
            {
                DateTime FromDay = DateTime.ParseExact(tuNgay, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime ToDay = DateTime.ParseExact(denNgay, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                var tonKho = await context.ChiTietPhieuNhaps
                    .Include(x => x.IdhhNavigation)
                    .Where(x => (x.IdpnNavigation.NgayNhap.Value.Date >= FromDay.Date && x.IdpnNavigation.NgayNhap.Value.Date <= ToDay)
                                && (idNhomHang == 0 || x.IdhhNavigation.Idnhh == idNhomHang)
                                && (idHangHoa == 0 || x.Idhh == idHangHoa))
                    .ToListAsync();
                var tonkho1 = tonKho.GroupBy(x => x.Idhh)
                    .Select(x => new
                    {
                        Id = x.Key,
                        MaHang = getMaHang((int)x.Key),
                        TenHang = getTenHang((int)x.Key),
                        TongSL = x.Sum(x => x.SoLuong),
                        TongTien = x.Sum(x => x.Gia * x.SoLuong)

                    })
                    .ToList();
                return Ok(tonkho1);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string getMaHang(int idhh)
        {
            HangHoa hangHoa = context.HangHoas.Find(idhh);
            return hangHoa.MaHh;
        }
        public string getTenHang(int idhh)
        {
            HangHoa hangHoa = context.HangHoas.Find(idhh);
            return hangHoa.TenHh;
        }
        [HttpPost("/TonKho/BaoCaoChiTiet")]
        public async Task<dynamic> getBaoCaoChiTiet(int idNCC, int idNhomHang, int idHangHoa, string tuNgay, string denNgay)
        {
            DateTime FromDay = DateTime.ParseExact(tuNgay, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime ToDay = DateTime.ParseExact(denNgay, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var tonKho = await context.ChiTietPhieuNhaps
                .Include(x => x.IdpnNavigation.IdnccNavigation)
                .Include(x => x.IdhhNavigation.IddvtNavigation)
                .Where(x => (x.IdpnNavigation.NgayNhap.Value.Date >= FromDay.Date && x.IdpnNavigation.NgayNhap.Value.Date <= ToDay)
                            && (idNhomHang == 0 || x.IdhhNavigation.Idnhh == idNhomHang)
                            && (idHangHoa == 0 || x.Idhh == idHangHoa)
                            && (idNCC == 0 || x.IdpnNavigation.Idncc == idNCC))
                .ToListAsync();
            var tonkho1 = tonKho;
            return tonkho1.Select(x => new
            {
                Id = x.Idctpn,
                NgayNhap = x.IdpnNavigation.NgayNhap.Value.ToString("dd-MM-yyyy"),
                NhaCungCap = x?.IdpnNavigation?.IdnccNavigation?.TenNcc,
                MaHang = x.IdhhNavigation?.MaHh,
                TenHang = x.IdhhNavigation?.TenHh,
                SoLuongNhap = x.SoLuong,
                SoLuongXuat = getSoLuongXuat((int)x.Idctpn),
                SoLuongTon = x.SoLuong,
                DonViTinh = x?.IdhhNavigation?.IddvtNavigation?.TenDvt,
                GiaNhap = x?.Gia,
                ThanhTien = x.Gia * x.SoLuong,
            });
        }
        public double getSoLuongXuat(int idCTPN)
        {
            List<ChiTietPhieuXuat> chiTietPhieuXuats = context.ChiTietPhieuXuats.Where(x => x.Idctpn == idCTPN)
                .ToList();
            double soLuong = 0;
            if (chiTietPhieuXuats.Count() > 0)
            {
                soLuong = (double)chiTietPhieuXuats.Sum(x => x.SoLuong);
            }
            return soLuong;
        }
    }
}
