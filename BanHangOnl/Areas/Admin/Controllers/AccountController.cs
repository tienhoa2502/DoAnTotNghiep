using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/TaiKhoan")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/TaiKhoan/ChiTiet")]
        public IActionResult Detail()
        {
            ViewBag.TaiKhoan = context.TaiKhoans.Include(x => x.IdvtNavigation).Where(x => x.Active == true).ToList();
            return View();
        }

        [HttpGet("/TaiKhoan/ViewThem")]
        public IActionResult viewAdd()
        {

            return View("Add");
        }
        [Route("/TaiKhoan/Them")]

        public IActionResult Add(TaiKhoan vaiTro)
        {
            vaiTro.Active = true;
            context.TaiKhoans.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Detail");
        }


        [Route("/TaiKhoan/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            TaiKhoan sua = context.TaiKhoans.Find(id);
            return View("Edit", sua);
        }

        [Route("/TaiKhoan/Sua")]

        public IActionResult Edit(TaiKhoan vaiTro)
        {
            TaiKhoan ncc = context.TaiKhoans.Find(vaiTro.Idtk);
            ncc.TenTk = vaiTro.TenTk;
            ncc.Pass = vaiTro.Pass;



            context.TaiKhoans.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Detail");
        }

        [Route("/TaiKhoan/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            TaiKhoan ncc = context.TaiKhoans.Find(id);
            ncc.Active = false;

            context.TaiKhoans.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Detail");
        }
    }
}
