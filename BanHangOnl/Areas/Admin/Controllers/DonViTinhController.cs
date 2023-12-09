using BanHangOnl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "NhanVien, QuanLy")]
	public class DonViTinhController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/DonViTinh")]
        public IActionResult Index()
        {
            ViewBag.DonViTinh = context.DonViTinhs.Where(x => x.Active == true).ToList();

            return View();
        }

        [HttpGet("/DonViTinh/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.DonViTinh = context.DonViTinhs.Where(x => x.Active == true).ToList();
            return View("Add");
        }

        [HttpGet("/DonViTinh/ViewThem")]
        public IActionResult Add()
        {

            return View("Add");
        }
        [Route("/DonViTinh/Them")]

        public IActionResult Add(DonViTinh vaiTro)
        {
            vaiTro.Active = true;
            context.DonViTinhs.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/DonViTinh/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            DonViTinh sua = context.DonViTinhs.Find(id);
            return View("Edit", sua);
        }

        [Route("/DonViTinh/Sua")]

        public IActionResult Edit(DonViTinh vaiTro)
        {
            DonViTinh ncc = context.DonViTinhs.Find(vaiTro.Iddvt);
            ncc.MaDvt = vaiTro.MaDvt;
            ncc.TenDvt = vaiTro.TenDvt;


            context.DonViTinhs.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/DonViTinh/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            DonViTinh ncc = context.DonViTinhs.Find(id);
            ncc.Active = false;

            context.DonViTinhs.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
       
        
    }
}
