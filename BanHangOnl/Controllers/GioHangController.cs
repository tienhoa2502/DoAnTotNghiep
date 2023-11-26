using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        // GET: Products
        public ActionResult Index()
        {
            //var items = context.HangHoas.ToList();

            return View();
        }

        [HttpGet("/ThanhToan")]
        public ActionResult ThanhToan()
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
	}
}
