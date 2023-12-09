using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "NhanVien, QuanLy")]
	public class SizeController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/Size")]
        public IActionResult Index()
        {
            ViewBag.Size = context.Sizes.Where(x => x.Active == true).ToList();

            return View();
        }

        [HttpGet("/Size/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.Size = context.Sizes.Where(x => x.Active == true).ToList();
            return View("Add");
        }

        [HttpGet("/Size/ViewThem")]
        public IActionResult Add()
        {

            return View("Add");
        }
        [Route("/Size/Them")]

        public IActionResult Add(Size vaiTro)
        {
            vaiTro.Active = true;
            context.Sizes.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/Size/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            Size sua = context.Sizes.Find(id);
            return View("Edit", sua);
        }

        [Route("/Size/Sua")]

        public IActionResult Edit(Size vaiTro)
        {
            Size ncc = context.Sizes.Find(vaiTro.Idsize);
            ncc.Size1 = vaiTro.Size1;



            context.Sizes.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/Size/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            Size ncc = context.Sizes.Find(id);
            ncc.Active = false;

            context.Sizes.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
