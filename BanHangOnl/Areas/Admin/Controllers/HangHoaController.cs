using BanHangOnl.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "NhanVien, QuanLy")]
	public class HangHoaController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HangHoaController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("/HangHoa")]
        public IActionResult Index()
        {
            ViewBag.HangHoa = context.HangHoas.Include(x => x.ImgHangHoas)
                .Include(x => x.IddvtNavigation)
                .Include(x => x.IdnhhNavigation)
                .Where(x => x.Active == true).ToList();
            return View();
        }
        [HttpGet("/HangHoa/Them")]
        public IActionResult ViewAdd()
        {
            ViewBag.NhomHangHoa = context.NhomHangHoas.Where(x => x.Active == true && x.Levels == 2).ToList();
            return View("Add");  
        }
        [Route("/HangHoa/Them")]
        public IActionResult Add(HangHoa vaiTro)
        {
            vaiTro.Active = true;
            vaiTro.HienThi = false;
            context.HangHoas.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/HangHoa/UpdateHienThi")]
        public void UpdateActive(int idHH)
        {
            HangHoa hh = context.HangHoas.Find(idHH);
            hh.HienThi = !hh.HienThi;
            context.HangHoas.Update(hh);
            context.SaveChanges();
        }



        [HttpPost("/HangHoa/updateHangHoa")]
        public dynamic addHangHoa(HangHoa hangHoa) 
        {
            if (hangHoa.Idhh == 0)
            {
                hangHoa.Active = true;
                hangHoa.HienThi = true;
                context.HangHoas.Add(hangHoa);
            }
            else
            {
                HangHoa hh = context.HangHoas.Find(hangHoa.Idhh);
                hh.MaHh = hangHoa.MaHh;
                hh.TenHh = hangHoa.TenHh;
                hh.Idnhh = hangHoa.Idnhh;
                hh.Iddvt = hangHoa.Iddvt;
                hh.Idsize = hangHoa.Idsize;
                hh.GiaBan = hangHoa.GiaBan;
                hh.GiaSale = hangHoa.GiaSale;
                context.HangHoas.Update(hh);

            }
            context.SaveChanges();
            return new
            {
                id =hangHoa.Idhh,
            };
        }
        [HttpPost("/HangHoa/UpdateAnh")]
        public IActionResult UpdateHanHoa([FromForm] List<IFormFile> files, [FromQuery] int idHangHoa)
        {
            HangHoa hangHoa = context.HangHoas.Find(idHangHoa);
            var listAnhCu = context.ImgHangHoas.Where(x => x.Idhh == idHangHoa).ToList();   
            if(listAnhCu.Count()> 0)
            {
                context.ImgHangHoas.RemoveRange(listAnhCu);
                context.SaveChanges();  
            }
            List<ImgHangHoa> imgHangHoas = new List<ImgHangHoa>();
            int i = 1;
            foreach (IFormFile formFile in files)
            {
                ImgHangHoa img = new ImgHangHoa();  
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img/HangHoa");
                string uniqueFileName = hangHoa.MaHh + " " + i + Path.GetExtension(formFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Lưu file ảnh vào thư mục /assets/img/avatars/
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                i++;
                img.Img = "/img/HangHoa/" + uniqueFileName;
                img.Idhh = idHangHoa;
                imgHangHoas.Add(img);
            }
            imgHangHoas[0].IsDefault = true;
            context.ImgHangHoas.AddRange(imgHangHoas);

            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Route("/HangHoa/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            HangHoa sua = context.HangHoas.Find(id);
            ViewBag.NhomHangHoa = context.NhomHangHoas.Where(x => x.Active == true && x.Levels == 2).ToList();
            ViewBag.Image = context.ImgHangHoas.Where(x => x.Idhh == id).ToList();
            //ViewBag.NhomHangHoa = context.NhomHangHoas.Where(x => x.Active == true).ToList();
            return View("Edit", sua);
        }



        [Route("/HangHoa/Sua")]

        public IActionResult Edit(HangHoa vaiTro)
        {
            HangHoa tt = context.HangHoas.Find(vaiTro.Idhh);

            tt.MaHh = vaiTro.MaHh;
            tt.TenHh = vaiTro.TenHh;
            tt.ImgHangHoas = vaiTro.ImgHangHoas;
            tt.TenHh = vaiTro.TenHh;
            tt.MaHh = vaiTro.MaHh;
            tt.TenHh = vaiTro.TenHh;
            //tt.Levels = vaiTro.Levels;

            context.HangHoas.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }




        [Route("/HangHoa/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            HangHoa tt = context.HangHoas.Find(id);
            tt.Active = false;

            context.HangHoas.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        
                   

    }
}
