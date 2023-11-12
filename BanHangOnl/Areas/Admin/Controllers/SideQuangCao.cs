using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SideQuangCaoController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/SideQuangCao")]
        public IActionResult Index()
        {
            ViewBag.SideQuangCao = context.SideQuangCaos.Where(x => x.Active == true).ToList();

            return View();
        }
        [HttpGet("/SideQuangCao/Them")]
        public IActionResult viewAdd()
        {

            return View("Add");
        }

        [Route("/SideQuangCao/Them")]

        public IActionResult Add(SideQuangCao vaiTro)
        {
            vaiTro.Active = true;
            context.SideQuangCaos.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/SideQuangCao/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            SideQuangCao ncc = context.SideQuangCaos.Find(id);
            ncc.Active = false;

            context.SideQuangCaos.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
