using BanHangOnl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "QuanLy")]
    public class AccountController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/TaiKhoan")]
        public IActionResult Index()
        {
            //ViewBag.TaiKhoan = context.TaiKhoans
            //    .Include(x => x.IdvtNavigation)
            //    .Where(x => x.Active == true).ToList();

            List<TaiKhoan> danhSachTaiKhoan = context.TaiKhoans
            .Include(x => x.IdvtNavigation)
            .Include(x => x.NhanViens)
            .Include(x => x.KhachHangs)
            .Where(x => x.Active == true)
            .ToList();

            foreach (var taiKhoan in danhSachTaiKhoan)
            {
                taiKhoan.Pass = "*****"; // Thay thế bằng logic mã hóa thực tế của bạn
            }

            ViewBag.DanhSachTaiKhoan = danhSachTaiKhoan;

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

            

            return View("Info",taiKhoan);
        }

        [HttpPost("/TaiKhoan/ThongTinCaNhan/{id}")]
        public IActionResult Info(int id, string currentPassword, string newPassword, string renewPassword)
        {
            // Lấy tài khoản từ database dựa trên id
            TaiKhoan taiKhoan = context.TaiKhoans.Find(id);

            // Kiểm tra mật khẩu hiện tại
            if (currentPassword == taiKhoan.Pass)
            {
                // Kiểm tra mật khẩu mới và nhập lại mật khẩu
                if (newPassword == renewPassword)
                {
                    // Cập nhật mật khẩu mới vào database
                    taiKhoan.Pass = newPassword;
                    context.SaveChanges();

                    // Redirect hoặc trả về thông báo thành công
                    return RedirectToAction("Info", new { id = id });
                }
                else
                {
                    // Trả về thông báo lỗi nếu mật khẩu mới không trùng khớp
                    ModelState.AddModelError("NewPassword", "Mật khẩu mới và nhập lại mật khẩu không khớp.");
                }
            }
            else
            {
                // Trả về thông báo lỗi nếu mật khẩu hiện tại không đúng
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
            }

            // Trả về view với thông báo lỗi
            return View("Info", taiKhoan);
        }





    }
}

