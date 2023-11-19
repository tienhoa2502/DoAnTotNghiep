using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
		QuanLyBanHangContext context = new QuanLyBanHangContext();
		// GET: Products

		//private readonly QuanLyBanHangContext _context;

		//public SanPhamController(QuanLyBanHangContext context)
		//{
		//	_context = context;
		//}
		[HttpGet("/SanPham/ChiTietSanPham")]
		public IActionResult Index()
        {
            return View();
        }
    }
}
