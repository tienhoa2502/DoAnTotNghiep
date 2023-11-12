using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
		public IActionResult Index()
		{
			//var angHoa = context.HangHoas.ToList();

			return View();
		}
	}
}
