using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/TaiKhoan")]
        public IActionResult Index()
        {
            ViewBag.TaiKhoan = context.TaiKhoans.Include(x => x.IdvtNavigation).Where(x => x.Active == true).ToList();

            return View();
        }

        [HttpGet("/TaiKhoan/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.TaiKhoan = context.TaiKhoans.Where(x => x.Active == true).ToList();
            return View("Add");
        }

        [HttpGet("/TaiKhoan/ViewThem")]
        public IActionResult Add()
        {

            return View("Add");
        }
        [Route("/TaiKhoan/Them")]

        public IActionResult Add(TaiKhoan vaiTro)
        {
            vaiTro.Active = true;
            context.TaiKhoans.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/TaiKhoan/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            TaiKhoan sua = context.TaiKhoans.Find(id);
            return View("Edit", sua);
        }

        [Route("/TaiKhoan/Sua")]

        public IActionResult Edit(TaiKhoan vaiTro)
        {
            TaiKhoan ncc = context.TaiKhoans.Find(vaiTro.Idtk);
            ncc.TenTk = vaiTro.TenTk;
            ncc.Pass = vaiTro.Pass;


            context.TaiKhoans.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/TaiKhoan/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            TaiKhoan ncc = context.TaiKhoans.Find(id);
            ncc.Active = false;

            context.TaiKhoans.Update(ncc);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        [Route("/TaiKhoan/ThongTinCaNhan/{id}")]
        public IActionResult Info(int id)
        {
            TaiKhoan xem = context.TaiKhoans.Find(id);

            var taiKhoan = context.TaiKhoans
             .Include(t => t.KhachHangs)
             .Include(t => t.NhanViens)
             .FirstOrDefault(t => t.Idtk == id);

            // Kiểm tra nếu không tìm thấy tài khoản
            if (taiKhoan == null)
            {
                return NotFound();
            }

            

            return View(taiKhoan);
        }

		//[HttpGet("/TaiKhoan/ThongTinCaNhan")]
		//public IActionResult Info()
		//{
		//     ViewBag.NhanVien = context.NhanViens.Include(x => x.IdtkNavigation).Where(x => x.Active == true).ToList();

		//     return View();
		//}




        
	}
}

