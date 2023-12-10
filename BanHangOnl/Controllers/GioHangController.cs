using BanHangOnl.Models;
using CodeMegaVNPay.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using CodeMegaVNPay.Services;
using System.Security.Policy;

namespace BanHangOnl.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        // GET: Products
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
        public ActionResult thucHienThanhToan([FromBody] List<ChiTietPhieuXuat> cts)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(cts);
            }
            else
            {
                return Ok(new
                {
                    statusCode = 403
                });
            }
            
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

            return Json(response);
        }
    }
    public class Cart
    {
        public int idHh { get; set; }
        public int sl { get; set; }
    }
}
