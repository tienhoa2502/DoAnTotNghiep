using BanHangOnl.Models;
using CodeMegaVNPay.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using CodeMegaVNPay.Services;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using BanHangOnl.Areas.Admin.Controllers;
using BanHangOnl.Models.Mapping;
using QuanLyNhaHang.Models.Mapping;
using System.Text.RegularExpressions;

namespace BanHangOnl.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        // GET: Products
        private class data{
            public KhachHang khach { get;set; }
            public List<ChiTietPhieuXuat> chiTietPhieus { get; set; }
        }
        data d = new data();
        public ActionResult Index()
        {
			//var items = context.HangHoas.ToList();
			// Lấy dữ liệu từ cookie
			string myCookieValue = HttpContext.Request.Cookies["cart"];
			string myCookieValue2 = HttpContext.Request.Cookies["MyCookieName"];

			// Sử dụng dữ liệu từ cookie
			if (!string.IsNullOrEmpty(myCookieValue))
			{
				ViewBag.Cart = JsonConvert.DeserializeObject<List<Cart>>(myCookieValue);
			}
			return View();
        }
		[HttpPost("/updateCookies")]
		public IActionResult updateCookies([FromBody]Cart item)
		{
            var cookieName = "cart";

            string myCookieValue = HttpContext.Request.Cookies[cookieName];
            Response.Cookies.Delete(cookieName);

            List<Cart> carts;
            if (!string.IsNullOrEmpty(myCookieValue))
            {
                carts = JsonConvert.DeserializeObject<List<Cart>>(myCookieValue);
            }
            else
            {
                carts = new List<Cart>();
            }
            var get = carts.FirstOrDefault(x => x.idHh == item.idHh);
            if (get == null)
            {
                carts.Add(item);
            }
            
            string json = JsonConvert.SerializeObject(carts);
            
            var expireDays = 7; // Số ngày cookie tồn tại
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(expireDays),
                HttpOnly = true,
                IsEssential = true // Chọn theo nhu cầu của bạn
            };

            Response.Cookies.Append(cookieName, json, option);
            return Ok(carts.Count);
		}
        [HttpPost("/getMaGiamGia")]
        public dynamic getMaGiamGia(string ma)
        {
            var voucher = context.Vouchers.FirstOrDefault(x => x.MaVoucher == ma);
            if (voucher != null)
            {
                return new
                {
                    statusCode = 200,
                    message = "Áp dụng mã thành công",
                    tyLeGiam = voucher.ApDung,
                };
            }
            return new
            {
                statusCode = 500,
                message = "Voucher hết hạn hoặc không tồn tại!",
            };
        }
        [HttpPost("/removeCookies")]
        public IActionResult removeCookies(int idHh)
        {
            var cookieName = "cart";

            string myCookieValue = HttpContext.Request.Cookies[cookieName];
            Response.Cookies.Delete(cookieName);

            List<Cart> carts;
            if (!string.IsNullOrEmpty(myCookieValue))
            {
                carts = JsonConvert.DeserializeObject<List<Cart>>(myCookieValue);
            }
            else
            {
                carts = new List<Cart>();
            }
            var get = carts.FirstOrDefault(x => x.idHh == idHh);
            if (get != null)
            {
                carts.Remove(get);
            }
            string json = JsonConvert.SerializeObject(carts);

            var expireDays = 7; // Số ngày cookie tồn tại
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(expireDays),
                HttpOnly = true,
                IsEssential = true // Chọn theo nhu cầu của bạn
            };

            Response.Cookies.Append(cookieName, json, option);
            return Ok(carts.Count);
        }
        [HttpPost("/getCookiesCount")]
        public IActionResult getCookiesCount()
        {
            var cookieName = "cart";

            string myCookieValue = HttpContext.Request.Cookies[cookieName];
            List<Cart> carts;
            if (!string.IsNullOrEmpty(myCookieValue))
            {
                carts = JsonConvert.DeserializeObject<List<Cart>>(myCookieValue);
            }
            else
            {
                carts = new List<Cart>();
            }
            return Ok(carts.Count);
        }

        [HttpGet("/ThanhToan")]
        public ActionResult ThanhToan()
        {
            //var items = context.HangHoas.ToList();

            return View();
        }
        [HttpPost("/thanhToanHoaDon")]
        public dynamic thucHienThanhToan([FromBody] List<ChiTietPhieuXuat> cts)
        {
            bool tt = KiemTraSoLuongCon(cts);
            if (!tt)
            {
                return new
                {
                    statusCode = 500 ,
                    message = "Không đủ số lượng hàng hóa trong kho",
                };
            }
            if (User.Identity.IsAuthenticated)
            {
                KhachHang nv = context.KhachHangs.Find(Int32.Parse(User.Identity.Name));
                HttpContext.Session.SetString("KhachHang", JsonConvert.SerializeObject(nv));
                HttpContext.Session.SetString("ChiTietXuat", JsonConvert.SerializeObject(GanTenChiTietXuat(cts)));
                string khachHangJson = HttpContext.Session.GetString("KhachHang");
                string chiTietXuatJson = HttpContext.Session.GetString("ChiTietXuat");

                return new
                {
                    kh = nv,
                    ctx = GanTenChiTietXuat(cts),
                    statusCode = 200,
                    message = "Thành công",
                }; 
            }
            else
            {
                return Ok(new
                {
                    statusCode = 403
                });
            }
            
        }
        [HttpPost("/ganTyLeGiam")]
        public void ganTyLeGiam(int tyleGiam)
        {
            HttpContext.Session.SetString("TyLeGiam", JsonConvert.SerializeObject(tyleGiam));
        }
        public bool KiemTraSoLuongCon(List<ChiTietPhieuXuat> cts)
        {
            bool tt = true;
            foreach (ChiTietPhieuXuat c in cts)
            {
                var tonKho = context.ChiTietPhieuNhaps
                    .Include(x => x.IdhhNavigation)
                    .Where(x => x.Idhh == c.Idhh && x.Idmau == c.Idmau && x.Idsize == c.Idsize)
                    .ToList();
                double slc = 0;
                if (tonKho.Count() != 0)
                {
                    double sl = (double)tonKho.Sum(x => x.SoLuong);
                    double slx = (double)tonKho.Sum(x => x.SoLuongXuat);
                    slc = sl - slx;
                }
                if (slc < c.SoLuong)
                {
                    tt = false; 
                    break;
                }
            }

            return tt;
        }
        public List<ChiTietPhieuXuat> GanTenChiTietXuat(List<ChiTietPhieuXuat> chiTietPhieuXuats)
        {
            foreach (ChiTietPhieuXuat c in chiTietPhieuXuats)
            {
                c.IdhhNavigation = getHH(c.Idhh);
            }
            return chiTietPhieuXuats;
        } 
        public dynamic getHH(int? idhh)
        {
            return context.HangHoas.Select(x => new HangHoa()
            {
                Idhh = x.Idhh,
                TenHh = x.TenHh,
                GiaBan = x.GiaBan,
                ImgHangHoas = x.ImgHangHoas.Select(x => new ImgHangHoa()
                {
                    Img = x.Img
                }
                ).ToList()
            }).FirstOrDefault(x => x.Idhh == idhh);
        }
        //[HttpGet("/ThanhToan")]
        //public ActionResult ThanhToan()
        //{
        //	//var items = context.HangHoas.ToList();

        //	return View();
        //}
        [HttpGet("/ThanhToanThanhCong")]
        public ActionResult ThanhToanSuccess()
        {
            //var items = context.HangHoas.ToList();

            return View();
        }

        //[HttpGet("/ThanhToan")]
        //public ActionResult ThanhToan()
        //{
        //	//var items = context.HangHoas.ToList();

        //	return View();
        //}


        private readonly IVnPayService _vnPayService;

        public GioHangController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            model.OrderDescription = " Thanh toán tại Code Mega";
            model.Name = "Code Mega";
            model.OrderType = "electric";
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if(response.VnPayResponseCode == "00")
            {
                var chiTietXuat = JsonConvert.DeserializeObject<List<ChiTietPhieuXuat>>(HttpContext.Session.GetString("ChiTietXuat"));
                var khachHang = JsonConvert.DeserializeObject<KhachHang>(HttpContext.Session.GetString("KhachHang"));
               var a  = luuPhieuXuat(khachHang, chiTietXuat);
                ViewBag.payment = a;
                if (a.Result.statusCode == 200)
                {
                    var cookieName = "cart";

                    string myCookieValue = HttpContext.Request.Cookies[cookieName];
                    Response.Cookies.Delete(cookieName);
                }
            }
            return View("ThanhToanSuccess");
        }
        public static string taoSoPhieuXuat(QuanLyBanHangContext context)
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("yyyyMMdd");
            var phieuNhap = context.PhieuXuats.Where(x => x.SoPx.Contains(date)).ToList();
            return $"PX-{date}-{(phieuNhap.Count() + 1).ToString("D2")}";
        }
        public async Task<dynamic> luuPhieuXuat(KhachHang khachHang, List<ChiTietPhieuXuat> chiTietPhieuXuats)
        {
            string cleanedInput = Regex.Replace(khachHang.Phone, @"\s+", "");
            // ty le giam
            var tylegiam = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("TyLeGiam"));

            // Sử dụng biểu thức chính quy để tìm số điện thoại
            khachHang.Phone = cleanedInput;
            List<ChiTietPhieuNhap> soLuongHhcon = await context.ChiTietPhieuNhaps
                .Include(x => x.IdpnNavigation)
                .Include(x => x.IdhhNavigation)
                .OrderBy(x => x.IdpnNavigation.NgayNhap).ToListAsync();
            var tran = context.Database.BeginTransaction();
            try
            {
                double tongTien = (double)chiTietPhieuXuats.Sum(x => x.SoLuong * x.Gia);
                double tienGiam = ((double)tylegiam / 100) * tongTien;
                double tongGiam = tongTien - tienGiam;
                TaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(x => x.TenTk == khachHang.Phone);
                List<KhachHang> a = context.KhachHangs.ToList();
                if (taiKhoan == null)
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk.TenTk = khachHang.Phone;
                    tk.Pass = khachHang.Phone;
                    tk.Active = true;
                    tk.Idvt = 7;
                    context.TaiKhoans.Add(tk);
                    context.SaveChanges();
                    taiKhoan = tk;
                    khachHang.MaKh = "KH" + (a.Count() + 1).ToString("D4");
                    khachHang.Idtk = tk.Idtk;
                    khachHang.Active = true;
                    context.KhachHangs.Add(khachHang);
                    context.SaveChanges();
                }
                PhieuXuat phieuXuat = new PhieuXuat();
                phieuXuat.Idkh = taiKhoan.Idtk;
                phieuXuat.NgayTao = DateTime.Now;
                phieuXuat.Active = true;
                phieuXuat.Idnv = 1;
                phieuXuat.SoPx = taoSoPhieuXuat(context);
                phieuXuat.DaGiao = false;
                phieuXuat.DonTra = false;
                phieuXuat.TyLeGiam = tylegiam;
                phieuXuat.TongTien = tongGiam;
                context.PhieuXuats.Add(phieuXuat);
                context.SaveChanges();

                foreach (ChiTietPhieuXuat t in chiTietPhieuXuats.ToList())
                {
                    double slq = (double)t.SoLuong;
                    foreach (ChiTietPhieuNhap slhhc in soLuongHhcon.Where(x => x.Idhh == t.Idhh && x.Idmau == t.Idmau && x.Idsize == t.Idsize && x.SoLuong != x.SoLuongXuat))
                    {
                        slhhc.SoLuongXuat = slhhc.SoLuongXuat ?? 0;
                        double slc = (double)((double)slhhc.SoLuong - slhhc.SoLuongXuat);
                        ChiTietPhieuXuat ct = new ChiTietPhieuXuat();
                        ct.Idhh = t.Idhh;
                        ct.Idpx = phieuXuat.Idpx;
                        ct.Gia = t.Gia;
                        ct.Idctpn = slhhc.Idctpn;
                        ct.Idmau = t.Idmau;
                        ct.Idsize = t.Idsize;
                        ct.Active = true;
                        //nếu mà trong kho còn nhiều hơn số xuất
                        if (slc > slq)
                        {
                            ct.SoLuong = t.SoLuong;
                            slhhc.SoLuongXuat += Convert.ToInt32(slq);
                            context.ChiTietPhieuXuats.Add(ct);
                            context.SaveChanges();
                            break;
                        }
                        //nếu mà trong kho ngang với số cần xuất
                        if (slc == slq)
                        {
                            ct.SoLuong = slc;
                            slhhc.SoLuongXuat += Convert.ToInt32(slq);
                            context.ChiTietPhieuXuats.Add(ct);
                            context.SaveChanges();
                            break;
                        }
                        //nếu trong kho còn ít hơn số cần xuất
                        if (slc < slq)
                        {
                            ct.SoLuong = (double)slc;
                            slq = (double)(slq - slc);
                            slhhc.SoLuongXuat += Convert.ToInt32(slc);
                            t.SoLuong = slq;
                            context.ChiTietPhieuXuats.Add(ct);
                            context.SaveChanges();
                        }
                        context.ChiTietPhieuNhaps.Update(slhhc);
                        context.SaveChanges();
                    }
                    chiTietPhieuXuats.Remove(t);
                    context.SaveChanges();
                }
                tran.Commit();
                return new
                {
                    statusCode = 200,
                    message = "Thành công",
                };
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return new
                {
                    statusCode = 500,
                    message = "Thất bại",
                };
            }

        }

    }
    public class Cart
    {
        public int idHh { get; set; }
        public int sl { get; set; }
    }
}
