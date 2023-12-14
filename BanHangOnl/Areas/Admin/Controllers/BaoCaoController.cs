using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelectPdf;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaoCaoController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet]
        public IActionResult Index()
        {
            return View("BaoCao/ChiTietPhieuXuatPDF");
        }
        [Route("/download/ChiTietPhieuXuat/{id:int}")]
        public IActionResult downloadPChiTietPhieuXuat(int id)
        {
            var fullView = new HtmlToPdf();
            fullView.Options.WebPageWidth = 1280;
            fullView.Options.PdfPageSize = PdfPageSize.A4;
            fullView.Options.MarginTop = 20;
            fullView.Options.MarginBottom = 20;
            fullView.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            var currentUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var pdf = fullView.ConvertUrl(currentUrl + "/ChiTietPhieuXuatPDF/" + id);

            var pdfBytes = pdf.Save();
            return File(pdfBytes, "application/pdf", "ChiTietPhieuXuat.pdf");
        }

        [Route("/ChiTietPhieuXuatPDF/{id:int}")]
        public IActionResult viewPDF(int id)
        {
            var phieu = context.PhieuXuats
                .Include(x => x.IdnvNavigation)
                .Include(x => x.ChiTietPhieuXuats)
                .Where(x => x.Idpx == id).FirstOrDefault();
            return View("ChiTietPhieuXuatPDF", phieu);
        }
    }
}
