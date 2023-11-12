using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhaCungCapController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/NhaCungCap")]
        public IActionResult Index()
        {
            ViewBag.NhaCungCap = context.NhaCungCaps.Where(x => x.Active == true).ToList();

            return View();
        }
        [HttpGet("/NhaCungCap/Them")]
        public IActionResult viewAdd()
        {

            return View("Add");
        }

        [Route("/NhaCungCap/Them")]

        public IActionResult Add(NhaCungCap vaiTro)
        {
            vaiTro.Active = true;
            context.NhaCungCaps.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/NhaCungCap/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            NhaCungCap sua = context.NhaCungCaps.Find(id);
            return View("Edit", sua);
        }

        [Route("/NhaCungCap/Sua")]

        public IActionResult Edit(NhaCungCap vaiTro)
        {
            NhaCungCap ncc = context.NhaCungCaps.Find(vaiTro.Idncc);
            ncc.MaNcc = vaiTro.MaNcc;
            ncc.TenNcc = vaiTro.TenNcc;
            ncc.DiaChi = vaiTro.DiaChi;
            ncc.DienThoai = vaiTro.DienThoai;
            ncc.Mail = vaiTro.Mail;
            ncc.GhiChu = vaiTro.GhiChu;


            context.NhaCungCaps.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/NhaCungCap/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            NhaCungCap ncc = context.NhaCungCaps.Find(id);
            ncc.Active = false;

            context.NhaCungCaps.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
