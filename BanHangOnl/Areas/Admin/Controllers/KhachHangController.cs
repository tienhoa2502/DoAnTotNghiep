using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Route("/KhachHang/ThongTinKhachHang/{id}")]
        public IActionResult Info(int id)
        {
            KhachHang xem = context.KhachHangs.Find(id);

            ViewBag.KhachHang = context.KhachHangs.Where(x => x.Active == true).ToList();
            return View();
        }

    }
}
