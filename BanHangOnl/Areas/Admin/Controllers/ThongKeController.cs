using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/ThongKe")]
        public IActionResult Index()
        {


            return View();
        }
    }
}
