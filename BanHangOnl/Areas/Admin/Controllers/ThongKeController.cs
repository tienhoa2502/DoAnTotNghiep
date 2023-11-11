using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/ThongKe")]
        public IActionResult Index()
        {


            return View();
        }
    }
}
