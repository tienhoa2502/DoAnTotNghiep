using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
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
    }
}
