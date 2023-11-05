using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangHoaController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/HangHoa")]
        public IActionResult Index()
        {
            ViewBag.HangHoa = context.HangHoas.Where(x => x.Active == true).ToList();
            return View();
        }
    }
}
