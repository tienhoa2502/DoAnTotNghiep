using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/TaiKhoan")]
        public IActionResult Index()
        {
            ViewBag.TaiKhoan = context.TaiKhoans.Include(x => x.IdvtNavigation).Where(x => x.Active == true).ToList();

            return View();
        }

        [HttpGet("/TaiKhoan/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.TaiKhoan = context.TaiKhoans.Where(x => x.Active == true).ToList();
            return View("Add");
        }

        [HttpGet("/TaiKhoan/ViewThem")]
        public IActionResult Add()
        {

            return View("Add");
        }
        [Route("/TaiKhoan/Them")]

        public IActionResult Add(TaiKhoan vaiTro)
        {
            vaiTro.Active = true;
            context.TaiKhoans.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        [Route("/TaiKhoan/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            TaiKhoan ncc = context.TaiKhoans.Find(id);
            ncc.Active = false;

            context.TaiKhoans.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }





        //[Route("/TaiKhoan/ThongTinCaNhan/{id}")]
        //public IActionResult viewInfo(int id)
        //{
        //    NhanVien xem = context.NhanViens.Find(id);

        //    ViewBag.NhanVien = context.NhanViens.Include(x => x.IdtkNavigation).Where(x => x.Active == true).ToList();
        //    return View("ThongTinCaNhan", xem);

        [HttpGet("/TaiKhoan/ThongTinCaNhan")]
        public IActionResult Info()
        {
             ViewBag.NhanVien = context.NhanViens.Include(x => x.IdtkNavigation).Where(x => x.Active == true).ToList();

             return View();
        }

        }
    }

