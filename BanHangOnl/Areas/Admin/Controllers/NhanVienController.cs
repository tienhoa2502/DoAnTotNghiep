using BanHangOnl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "QuanLy")]
    public class NhanVienController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/NhanVien")]
        public IActionResult Index()
        {
            ViewBag.NhanVien = context.NhanViens.Where(x => x.Active == true).ToList();

            return View();
        }

        [HttpGet("/NhanVien/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.NhanVien = context.NhanViens.Where(x => x.Active == true).ToList();
            return View("Add");
        }

        [HttpGet("/NhanVien/ViewThem")]
        public IActionResult Add()
        {

            return View("Add");
        }
        [Route("/NhanVien/Them")]

        public IActionResult Add(NhanVien vaiTro)
        {
            vaiTro.Active = true;
            context.NhanViens.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/NhanVien/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            NhanVien sua = context.NhanViens.Find(id);
            return View("Edit", sua);
        }

        [Route("/NhanVien/Sua")]

        public IActionResult Edit(NhanVien vaiTro)
        {
            NhanVien nv = context.NhanViens.Find(vaiTro.Idnv);
            nv.MaMv = vaiTro.MaMv;
            nv.Ten = vaiTro.Ten;            
            nv.GioiTinh = vaiTro.GioiTinh;            
            nv.Sdt = vaiTro.Sdt;            
            nv.QueQuan = vaiTro.QueQuan;
            nv.DiaChi = vaiTro.DiaChi;
            nv.Email = vaiTro.Email;


            context.NhanViens.Update(nv);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/NhanVien/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            NhanVien ncc = context.NhanViens.Find(id);
            ncc.Active = false;

            context.NhanViens.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
