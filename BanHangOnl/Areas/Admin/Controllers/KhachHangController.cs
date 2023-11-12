using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]   
    
    public class KhachHangController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/KhachHang")]
        public IActionResult Index()
        {
            ViewBag.KhachHang = context.KhachHangs.Where(x => x.Active == true).ToList();
            return View();
        }

        [HttpGet("/KhachHang/ThongTinKhachHang")]
        public IActionResult Info()
        {
            ViewBag.KhachHang = context.KhachHangs.Where(x => x.Active == true).ToList();
            return View();
        }
    }
}
