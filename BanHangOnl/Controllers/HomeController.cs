using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BanHangOnl.Controllers
{
    public class HomeController : Controller
    {
		QuanLyBanHangContext context = new QuanLyBanHangContext();

		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			ViewBag.HangHoa = context.HangHoas
				.Include(x => x.ImgHangHoas)
				.Where(x => x.HienThi == true && x.Active == true ).ToList();

			
				List<string> imageUrls = context.SideQuangCaos.Select(s => s.Img).ToList();
				ViewBag.ImageUrls = imageUrls;
			
			return View();
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet("/ThongTinCaNhan/{id}")]

        public IActionResult Index(int id)
        {
            //đổi lại người dùng
            var hangHoa = context.HangHoas
                             .Include(hh => hh.IddvtNavigation) // Đảm bảo navigation properties được load
                             .Include(hh => hh.IdnhhNavigation)
                             .FirstOrDefault(hh => hh.Idhh == id);
          
            if (hangHoa == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy sản phẩm
            }



            return View("ThongTinCaNhan", hangHoa);
        }


    }
}