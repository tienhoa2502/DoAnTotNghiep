using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhomTinTucController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/NhomTinTuc")]
        public IActionResult Index()
        {
            ViewBag.NhomTinTuc = context.NhomTinTucs.Where(x => x.Active == true).ToList();

            return View();
        }
        [HttpGet("/NhomTinTuc/Them")]
        public IActionResult viewAdd()
        {

            return View("Add");
        }
        [Route("/NhomTinTuc/Them")]

        public IActionResult Add(NhomTinTuc vaiTro)
        {
            vaiTro.Active = true;
            context.NhomTinTucs.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/NhomTinTuc/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            NhomTinTuc sua = context.NhomTinTucs.Find(id);
            return View("Edit", sua);
        }

        [Route("/NhomTinTuc/Sua")]

        public IActionResult Edit(NhomTinTuc vaiTro)
        {
            NhomTinTuc tt = context.NhomTinTucs.Find(vaiTro.Idntt);
            tt.TenNtt = vaiTro.TenNtt;
            //ncc.DienThoai = vaiTro.DienThoai;
            //ncc.Mail = vaiTro.Mail;
            //ncc.GhiChu = vaiTro.GhiChu;


            context.NhomTinTucs.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/TinTuc/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            NhomTinTuc tt = context.NhomTinTucs.Find(id);
            tt.Active = false;

            context.NhomTinTucs.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
