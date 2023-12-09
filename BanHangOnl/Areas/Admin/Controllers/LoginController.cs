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


        
        [HttpGet("/DangNhap")]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost("/DangNhapTaiKhoan")]
        [Authorize]
        public async Task<IActionResult> DangNhap(string UserName, string PassWord)
        {
            if (UserName == null || PassWord == null)
            {
                return Ok(new
                {
                    statusCode = 500,
                    message = "Đăng nhập thất bại!"
                });
            }
            else
            {
                var taiKhoan = context.TaiKhoans
                    .Include(x => x.IdvtNavigation)
                    .FirstOrDefault(x => x.TenTk.ToLower() == UserName.ToLower() && x.Pass == PassWord);

                var claims = new List<Claim>();
                if (taiKhoan != null)
                {
                    bool isNhanVien = taiKhoan.IdvtNavigation?.NhanVien == true ? true : false;
                    bool isQuanLy = taiKhoan.IdvtNavigation?.QuanLy == true ? true : false;

                    if (isNhanVien)
                    {
                        var user = context.NhanViens.FirstOrDefault(x => x.Idtk == taiKhoan.Idtk);
                        if (user != null)
                        {
                            claims.Add(new Claim(ClaimTypes.Name, user.Idnv.ToString(), user.Ten));
                            if (isQuanLy)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, "QuanLy"));
                                claims.Add(new Claim("VaiTro", taiKhoan.Idvt.ToString(), "1"));
                            }
                            else
                            {
                                claims.Add(new Claim(ClaimTypes.Role, "NhanVien"));
                                claims.Add(new Claim("VaiTro", taiKhoan.Idvt.ToString(), "2"));

                            }

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                            new AuthenticationProperties
                            {
                                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                                IsPersistent = false
                            });
                            //return RedirectToAction("Index", "Admin");
                            return Ok(new
                            {
                                statusCode = 200,
                                message = "Đăng nhập thành công!",
                                url = "/Admin"
                            });
                        }
                        else
                        {
                            return Ok(new
                            {
                                statusCode = 500,
                                message = "Đăng nhập thất bại!"
                            });
                        }
                    }
                    else
                    {
                        var user = context.KhachHangs.FirstOrDefault(x => x.Idtk == taiKhoan.Idtk);
                        if (user != null)
                        {
                            claims.Add(new Claim(ClaimTypes.Name, user.Idkh.ToString(), user.TenKh));
                            claims.Add(new Claim(ClaimTypes.Role, "KhachHang"));
                            claims.Add(new Claim("VaiTro", taiKhoan.Idvt.ToString(), "0"));

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                            new AuthenticationProperties
                            {
                                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                                IsPersistent = false
                            });
                            return Ok(new
                            {
                                statusCode = 200,
                                message = "Đăng nhập thành công!",
                                url = "/"
                            });
                        }
                        else
                        {
                            return Ok(new
                            {
                                statusCode = 500,
                                message = "Đăng nhập thất bại!"
                            });
                        }
                        
                    }
                }
                else
                {
                    return Ok(new
                    {
                        statusCode = 500,
                        message = "Tài khoản hoặc mật khẩu không chính xác!"
                    });
                }
            }

        }


        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/DangNhap");
        }




        //[HttpPost]
        //public async Task<IActionResult> Login(string username, string password, bool remember)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(username, password, remember, lockoutOnFailure: false);

        //    if (result.Succeeded)
        //    {
        //        // Đăng nhập thành công, chuyển hướng đến trang người dùng
        //        return RedirectToAction("/Home", "User");
        //    }
        //    else
        //    {
        //        // Đăng nhập thất bại, hiển thị thông báo lỗi
        //        ViewBag.Error = "Invalid username or password.";
        //        return View();
        //    }
        //}


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






        [HttpGet("/DangKy")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("/DangKy")]

        public IActionResult Register(KhachHang kh)
        {
            kh.Active = true;

            context.KhachHangs.Add(kh);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
