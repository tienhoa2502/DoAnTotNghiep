using BanHangOnl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "NhanVien, QuanLy")]
	public class MauController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/Mau")]
        public IActionResult Index()
        {
            ViewBag.Mau = context.Maus.Where(x => x.Active == true).ToList();

            return View();
        }

        [HttpGet("/Mau/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.Mau = context.Maus.Where(x => x.Active == true).ToList();
            return View("Add");
        }

        [HttpGet("/Mau/ViewThem")]
        public IActionResult Add()
        {

            return View("Add");
        }
        [Route("/Mau/Them")]

        public IActionResult Add(Mau vaiTro)
        {
            vaiTro.Active = true;
            context.Maus.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/Mau/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            Mau sua = context.Maus.Find(id);
            return View("Edit", sua);
        }

        [Route("/Mau/Sua")]

        public IActionResult Edit(Mau vaiTro)
        {
            Mau ncc = context.Maus.Find(vaiTro.Idmau);
            ncc.Mau1 = vaiTro.Mau1;




            context.Maus.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/Mau/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            Mau ncc = context.Maus.Find(id);
            ncc.Active = false;

            context.Maus.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
