using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        [HttpGet("/Voucher")]
        public IActionResult Index()
        {
            ViewBag.Voucher = context.Vouchers.ToList();

            return View();
        }
        [HttpGet("/Voucher/Them")]
        public IActionResult viewAdd()
        {

            return View("Add");
        }
        [Route("/Voucher/Them")]

        public IActionResult Add(Voucher vaiTro)
        {
            vaiTro.Active = true;
            context.Vouchers.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("/Voucher/UpdateAcTive")]
        public void UpdateActive(int idVC)
        {
            Voucher vc = context.Vouchers.Find(idVC);
            vc.Active = !vc.Active;
            context.Vouchers.Update(vc);
            context.SaveChanges();
        }



        [Route("/Voucher/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            Voucher sua = context.Vouchers.Find(id);
            return View("Edit", sua);
        }

        [Route("/Voucher/Sua")]

        public IActionResult Edit(Voucher vaiTro)
        {
            Voucher tt = context.Vouchers.Find(vaiTro.Id);
            tt.MaVoucher = vaiTro.MaVoucher;
            tt.TenVoucher = vaiTro.TenVoucher;
            tt.NgayBd = vaiTro.NgayBd;
            tt.NgayKt  = vaiTro.NgayKt;

            //ncc.DienThoai = vaiTro.DienThoai;
            //ncc.Mail = vaiTro.Mail;
            //ncc.GhiChu = vaiTro.GhiChu;


            context.Vouchers.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/Voucher/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            Voucher tt = context.Vouchers.Find(id);
            tt.Active = false;

            context.Vouchers.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
