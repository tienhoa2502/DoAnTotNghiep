using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BanHangOnl.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
		// GET: Products

		//private readonly QuanLyBanHangContext _context;

		//public SanPhamController(QuanLyBanHangContext context)
		//{
		//	_context = context;
		//}
		[HttpGet("/SanPham")]
		public IActionResult Index()
		{
			ViewBag.HangHoa = context.HangHoas
				.Include(x => x.ImgHangHoas)
				.Where(x => x.Active == true).ToList();
			return View();
		}
	}
}
