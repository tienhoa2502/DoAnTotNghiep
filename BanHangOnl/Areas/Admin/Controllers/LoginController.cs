using BanHangOnl.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]

    public class LoginController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        private readonly SignInManager<IdentityUser> _signInManager;


        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool remember)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, remember, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Đăng nhập thành công, chuyển hướng đến trang người dùng
                return RedirectToAction("/Home", "User");
            }
            else
            {
                // Đăng nhập thất bại, hiển thị thông báo lỗi
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Index");
        //}
        //private async Task SignInUser(TaiKhoan accounts)
        //{
        //    QuanLyNhaHangContext context = new QuanLyBanHangContext();
        //    NhanVien user = context.NhanVien.Where(x => x.Idtk == accounts.Idtk).FirstOrDefault();

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Idnv.ToString()),
        //        new Claim(ClaimTypes.Role, accounts.IdvtNavigation.Idvt.ToString()),
        //    };

        //    var claimsIdentity = new ClaimsIdentity(
        //        claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(claimsIdentity));
        //}

        //public IActionResult test()
        //{
        //    return View();
        //}






        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
