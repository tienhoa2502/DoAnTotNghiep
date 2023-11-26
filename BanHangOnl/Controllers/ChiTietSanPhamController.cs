using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //private readonly QuanLyBanHangContext _context; // Thay "YourDbContext" bằng tên thực của DbContext của bạn.

        //public ChiTietSanPhamController(QuanLyBanHangContext context)
        //{
        //    _context = context;
        //}
        [HttpGet("/SanPham/ChiTietSanPham/{id}")]

        public IActionResult Index( int id)
        {
            var hangHoa = context.HangHoas
                             .Include(hh => hh.IddvtNavigation) // Đảm bảo navigation properties được load
                             .Include(hh => hh.IdnhhNavigation)
                             .FirstOrDefault(hh => hh.Idhh == id);
            //ViewBag.Mau = context.ChiTietPhieuNhaps.Where(x => x.Idhh == id && x.SoLuong != x.so)
            ViewBag.Img = context.ImgHangHoas.Where(x => x.Idhh == id).ToList();
            ViewBag.ImgD = context.ImgHangHoas.FirstOrDefault(x => x.Idhh == id && x.IsDefault == true).Img;
            if (hangHoa == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy sản phẩm
            }



            return View("Index" , hangHoa);
        }
        
    }
}
