using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class PhieuNhapController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/PhieuNhap")]
        public IActionResult Index()
        {
            ViewBag.PhieuNhap = context.PhieuNhaps
                .Include(x => x.ChiTietPhieuNhaps)
                .Include(x => x.IdnvNavigation).Where(x => x.Active == true).ToList();
            return View();
        }
        [HttpGet("/PhieuNhap/Xem")]
        public IActionResult Info()
        {

            return View();
        }

        [HttpGet("/PhieuNhap/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.NhaCungCap = context.NhaCungCaps.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();
            ViewBag.NhanVien = context.NhanViens.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();
            return View("Add");
        }

        [Route("/PhieuNhap/Them")]

        public IActionResult Add(PhieuNhap vaiTro)
        {

            ViewBag.PhieuNhap = context.PhieuNhaps
                .Include(x => x.ChiTietPhieuNhaps)
                .Include(x => x.IdnvNavigation).Where(x => x.Active == true).ToList();

            ViewBag.CHiTietPhieuNhap = context.ChiTietPhieuNhaps.Where(x => x.Active == true).ToList();

            ViewBag.NhanVien = context.NhanViens.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();

            vaiTro.Active = true;
            vaiTro.NgayNhap = DateTime.Now;
            //vaiTro.NgaySua = DateTime.Now;
            context.PhieuNhaps.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/PhieuNhap/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            PhieuNhap sua = context.PhieuNhaps.Find(id);
            return View("Edit", sua);
        }

        [Route("/PhieuNhap/Sua")]

        public IActionResult Edit(PhieuNhap vaiTro)
        {
            PhieuNhap tt = context.PhieuNhaps.Find(vaiTro.Idpn);
            //tt.Img = vaiTro.Img;
            //tt.TenTt = vaiTro.TenTt;
            //tt.ChiTiet = vaiTro.ChiTiet;

            

            context.PhieuNhaps.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/PhieuNhap/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            PhieuNhap tt = context.PhieuNhaps.Find(id);
            tt.Active = false;

            context.PhieuNhaps.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
