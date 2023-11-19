using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TinTucController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/TinTuc")]
        public IActionResult Index()
        {
            ViewBag.TinTuc = context.TinTucs.Include(x => x.IdnttNavigation).ToList();

            return View();
        }
        [HttpGet("/TinTuc/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.NhomTinTuc = context.NhomTinTucs.Where(x => x.Active == true /*&& x.Levels == 2)*/).ToList();
            return View("Add");
        }

        [Route("/TinTuc/Them")]
        public IActionResult Add(TinTuc vaiTro)
        {
            vaiTro.Active = true;
            vaiTro.NgayTao = DateTime.Now;
            vaiTro.NgaySua = DateTime.Now;
            context.TinTucs.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost("/TinTuc/UpdateAcTive")]
        public void UpdateActive(int idTT)
        {
            TinTuc tin = context.TinTucs.Find(idTT);
            tin.Active = !tin.Active;
            context.TinTucs.Update(tin);
            context.SaveChanges();  
        }


        [Route("/TinTuc/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            ViewBag.NhomTinTuc = context.NhomTinTucs.Where(x => x.Active == true /*&& x.Levels == 2)*/).ToList();

            TinTuc sua = context.TinTucs.Find(id);
            return View("Edit", sua);
        }

        [Route("/TinTuc/Sua")]

        public IActionResult Edit(TinTuc vaiTro)
        {
            TinTuc tt = context.TinTucs.Find(vaiTro.Idtt);
            tt.Img = vaiTro.Img;
            tt.TenTt = vaiTro.TenTt;
            tt.ChiTiet = vaiTro.ChiTiet;



            context.TinTucs.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/TinTuc/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            TinTuc tt = context.TinTucs.Find(id);
            tt.Active = false;

            context.TinTucs.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
