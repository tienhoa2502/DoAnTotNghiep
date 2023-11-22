using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhomHangHoaController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/NhomHangHoa")]
        public IActionResult Index()
        {
            ViewBag.NhomHangHoa = context.NhomHangHoas.Where(x => x.Active == true).ToList();
            return View();
        }

        [HttpGet("/NhomHangHoa/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.NhomHHCap1 = context.NhomHangHoas.Where(x => x.Levels == 1 && x.Active == true).ToList();
            return View("Add");
        }
        [Route("/NhomHangHoa/Them")]

        public IActionResult Add(NhomHangHoa vaiTro)
        {
            vaiTro.Active = true;
            vaiTro.HienThi = true;
            context.NhomHangHoas.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/NhomHangHoa/UpdateHienThi")]
        public void UpdateActive(int idNHH)
        {
            NhomHangHoa nhh = context.NhomHangHoas.Find(idNHH);
            nhh.HienThi = !nhh.HienThi;
            context.NhomHangHoas.Update(nhh);
            context.SaveChanges();
        }



        [Route("/NhomHangHoa/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            NhomHangHoa sua = context.NhomHangHoas.Find(id);

            ViewBag.NhomHHCap1 = context.NhomHangHoas.Where(x => x.Levels == 1 && x.Active == true).ToList();
            return View("Edit", sua);
        }

        [Route("/NhomHangHoa/Sua")]

        public IActionResult Edit(NhomHangHoa vaiTro)
        {
            NhomHangHoa tt = context.NhomHangHoas.Find(vaiTro.Idnhh);
            tt.MaNhh = vaiTro.MaNhh;
            tt.TenNhh = vaiTro.TenNhh;
            tt.Levels = vaiTro.Levels;



            context.NhomHangHoas.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/NhomHangHoa/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            NhomHangHoa tt = context.NhomHangHoas.Find(id);
            tt.Active = false;

            context.NhomHangHoas.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
