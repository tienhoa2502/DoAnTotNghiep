using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "NhanVien, QuanLy")]
    public class HomeController : Controller
    {
        [HttpGet("/Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
