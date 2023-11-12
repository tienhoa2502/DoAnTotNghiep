using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VaiTroController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/VaiTro")]
        public IActionResult Index()
        {
            ViewBag.VaiTro = context.VaiTros.Where(x => x.Active == true).ToList();

            return View();
        }

        [HttpGet("/VaiTro/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.VaiTro = context.VaiTros.Where(x => x.Active == true).ToList();
            return View();
        }

        [HttpGet("/VaiTro/ViewThem")]
        public IActionResult Add()
        {

            return View("Add");
        }
        [Route("/VaiTro/Them")]

        public IActionResult Add(VaiTro vaiTro)
        {
            vaiTro.Active = true;
            context.VaiTros.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Detail");
        }


        [Route("/VaiTro/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            VaiTro sua = context.VaiTros.Find(id);
            return View("Edit", sua);
        }

        [Route("/VaiTro/Sua")]

        public IActionResult Edit(VaiTro vaiTro)
        {
            VaiTro ncc = context.VaiTros.Find(vaiTro.Idvt);
            ncc.MaVt = vaiTro.MaVt;
            ncc.TenVt = vaiTro.TenVt;



            context.VaiTros.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Detail");
        }

        [Route("/VaiTro/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            VaiTro ncc = context.VaiTros.Find(id);
            ncc.Active = false;

            context.VaiTros.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Detail");
        }
    }
}
