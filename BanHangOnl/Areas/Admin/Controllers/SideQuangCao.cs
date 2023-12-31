﻿using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "NhanVien, QuanLy")]
	public class SideQuangCaoController : Controller
    {
        
        QuanLyBanHangContext context = new QuanLyBanHangContext();
		private readonly IWebHostEnvironment _hostingEnvironment;
		public SideQuangCaoController(IWebHostEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}
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

        [HttpPost("/SideQuangCao/Them")]

        public IActionResult Add([FromForm] IFormFile files)
        {
			SideQuangCao sideQuangCao = new SideQuangCao();
			string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img/Slide");
			string uniqueFileName = DateTime.Now.ToString("HH-mm-ss") + Path.GetExtension(files.FileName);
			string filePath = Path.Combine(uploadsFolder, uniqueFileName);

			// Lưu file ảnh vào thư mục /assets/img/avatars/
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				files.CopyTo(fileStream);
			}
            sideQuangCao.Img = "/img/Slide/" + uniqueFileName;
            sideQuangCao.HienThi = true;
            sideQuangCao.NgayTao = DateTime.Now;
			sideQuangCao.Active = true;
            context.SideQuangCaos.Add(sideQuangCao);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/SideQuangCao/UpdateAcTive")]
        public void UpdateActive(int idSL)
        {
            SideQuangCao sl = context.SideQuangCaos.Find(idSL);
            sl.HienThi = !sl.HienThi;
            context.SideQuangCaos.Update(sl);
            context.SaveChanges();
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
