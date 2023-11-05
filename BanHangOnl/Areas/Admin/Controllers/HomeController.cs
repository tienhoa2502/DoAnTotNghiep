using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [HttpGet("/Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
