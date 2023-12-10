using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "NhanVien, QuanLy")]
	public class NhomTinTucController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
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
            vaiTro.HienThi = true;

            context.NhomTinTucs.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost("/NhomTinTuc/UpdateAcTive")]
        public void UpdateActive(int idNTT)
        {
            NhomTinTuc nhomtin = context.NhomTinTucs.Find(idNTT);
            nhomtin.HienThi = !nhomtin.HienThi;
            context.NhomTinTucs.Update(nhomtin);
            context.SaveChanges();
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
