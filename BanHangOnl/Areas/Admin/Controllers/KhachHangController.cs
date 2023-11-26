using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]   
    
    public class KhachHangController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/KhachHang")]
        public IActionResult Index()
        {
            ViewBag.KhachHang = context.KhachHangs.Where(x => x.Active == true).ToList();
            return View();
        }

        [Route("/KhachHang/ThongTinKhachHang/{id}")]
        public IActionResult Info(int id)
        {
            KhachHang xem = context.KhachHangs.Find(id);
            List<PhieuXuat> PhieuXuats = context.PhieuXuats
                .Include(x => x.ChiTietPhieuXuats)
                .ThenInclude(x => x.IdmauNavigation)
                    .Include(x => x.ChiTietPhieuXuats)
                    .ThenInclude(x => x.IdsizeNavigation)
                    .Include(x => x.ChiTietPhieuXuats)
                    .ThenInclude(x => x.IdhhNavigation)
                .Where(x => x.Idkh == id).ToList();
            ViewBag.DonDangGiao = PhieuXuats.Where(x => x.DaGiao != true);
            ViewBag.LichSuDon = PhieuXuats.Where(x => x.DaGiao == true);
            ViewBag.KhachHang = context.KhachHangs.Where(x => x.Idtk== id && x.Active == true).ToList();
            return View();
        }

    }
}
