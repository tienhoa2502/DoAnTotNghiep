using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CtkmController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/ChuongTrinhKhuyenMai")]
        public IActionResult Index()
        {
            ViewBag.Ctkm = context.Ctkms.Where(x => x.Active == true).ToList();
            return View();
        }
        [HttpGet("/ChuongTrinhKhuyenMai/Them")]
        public IActionResult viewAdd()
        {

            return View("Add");
        }
        [Route("/ChuongTrinhKhuyenMai/Them")]

        public IActionResult Add(Ctkm vaiTro)
        {
            vaiTro.Active = true;
            //vaiTro.NgayTao = DateTime.Now;
            //vaiTro.NgaySua = DateTime.Now;
            context.Ctkms.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/ChuongTrinhKhuyenMai/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            Ctkm sua = context.Ctkms.Find(id);
            return View("Edit", sua);
        }

        [Route("/ChuongTrinhKhuyenMai/Sua")]

        public IActionResult Edit(Ctkm vaiTro)
        {
            Ctkm tt = context.Ctkms.Find(vaiTro.Id);
            //tt.Img = vaiTro.Img;
            //tt.TenTt = vaiTro.TenTt;
            //tt.ChiTiet = vaiTro.ChiTiet;



            context.Ctkms.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/ChuongTrinhKhuyenMai/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            Ctkm tt = context.Ctkms.Find(id);
            tt.Active = false;

            context.Ctkms.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
