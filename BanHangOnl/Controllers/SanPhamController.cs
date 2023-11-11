using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
		// GET: Products

		//private readonly QuanLyNhaHangContext _context;

		//public SanPhamController(QuanLyNhaHangContext context)
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
