using BanHangOnl.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangHoaController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HangHoaController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("/HangHoa")]
        public IActionResult Index()
        {
            ViewBag.HangHoa = context.HangHoas.Where(x => x.Active == true).ToList();
            return View();
        }
        [HttpGet("/HangHoa/Them")]
        public IActionResult ViewAdd()
        {
            return View("Add");  
        }
        [HttpPost("/HangHoa/updateHangHoa")]
        public dynamic addHangHoa(HangHoa hangHoa) {
            return new
            {
                id = 1,
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
                img.IsDefault = true;
                imgHangHoas.Add(img);
            }
            context.ImgHangHoas.AddRange(imgHangHoas);

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
