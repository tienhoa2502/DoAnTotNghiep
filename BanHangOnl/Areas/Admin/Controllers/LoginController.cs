using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/Login")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
