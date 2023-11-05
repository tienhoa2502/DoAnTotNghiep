using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    public class VoucherController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/Voucher")]
        public IActionResult Index()
        {
            ViewBag.Voucher = context.Vouchers.Where(x => x.Active == true).ToList();

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


        //[Route("/Voucher/ViewSua/{id}")]
        //public IActionResult viewEdit(int id)
        //{
        //    TinTuc sua = context.Vouchers.Find(id);
        //    return View("Edit", sua);
        //}

        //[Route("/TinTuc/Sua")]

        //public IActionResult Edit(Voucher vaiTro)
        //{
        //    TinTuc tt = context.Vouchers.Find(vaiTro.Id);
        //    tt.Img = vaiTro.t;
        //    tt.TenTt = vaiTro.TenTt;
        //    tt.ChiTiet = vaiTro.ChiTiet;
        //    //ncc.DienThoai = vaiTro.DienThoai;
        //    //ncc.Mail = vaiTro.Mail;
        //    //ncc.GhiChu = vaiTro.GhiChu;


        //    context.TinTucs.Update(tt);
        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //[Route("/Voucher/Xoa/{id}")]
        //public IActionResult Xoa(int id)
        //{
        //    Voucher tt = context.Vouchers.Find(id);
        //    tt.Active = false;

        //    context.Vouchers.Update(tt);
        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
