using BanHangOnl.Models;
using CodeMegaVNPay.Models;
using Microsoft.AspNetCore.Mvc;
using CodeMegaVNPay.Services;
using System.Security.Policy;

namespace BanHangOnl.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        // GET: Products
        public ActionResult Index()
        {
            //var items = context.HangHoas.ToList();

            return View();
        }

        [HttpGet("/ThanhToan")]
        public ActionResult ThanhToan()
        {
            //var items = context.HangHoas.ToList();

            return View();
        }

		[HttpGet("/ThanhToanThanhCong")]
		public ActionResult ThanhToanSuccess()
		{
			//var items = context.HangHoas.ToList();

			return View();
		}

		//[HttpGet("/ThanhToan")]
		//public ActionResult ThanhToan()
		//{
		//	//var items = context.HangHoas.ToList();

		//	return View();
		//}


		private readonly IVnPayService _vnPayService;

        public GioHangController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            model.OrderDescription = " Thanh toán tại Code Mega";
            model.Name = "Code Mega";
            model.OrderType = "electric";
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
}
