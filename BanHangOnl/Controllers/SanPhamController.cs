﻿using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BanHangOnl.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
		// GET: Products

		//private readonly QuanLyBanHangContext _context;

		//public SanPhamController(QuanLyBanHangContext context)
		//{
		//	_context = context;
		//}
		[HttpGet("/SanPham")]
		public IActionResult Index()
		{
			ViewBag.HangHoa = context.HangHoas
				.Include(x => x.ImgHangHoas)
				.Where(x => x.HienThi == true && x.Active == true).ToList();

            ViewBag.NhomHangHoa = context.NhomHangHoas.Where(x => x.Active == true && x.HienThi == true).ToList();
            ViewBag.Voucher = context.Vouchers.Where(x => x.Active == true && x.HienThi == true).ToList();

            ViewBag.NhomHangHoaCap1 = context.NhomHangHoas.Where(x => x.Active == true && x.Levels == 1 && x.HienThi == true).ToList();
            ViewBag.NhomHangHoaCap2 = context.NhomHangHoas.Where(x => x.Active == true && x.Levels == 2 && x.HienThi == true).ToList();

            return View();
		}

		[HttpGet("/NhomSanPham/{TenNhh}")]
		public IActionResult NhomSP(string TenNhh)
		{
            NhomHangHoa nhomHangHoa = context.NhomHangHoas
				.FirstOrDefault(x => x.TenNhh == TenNhh && x.Active == true && x.HienThi == true);
            if (nhomHangHoa == null)
            {
                // Handle the case where the product category is not found
                return NotFound();
            }

            List<HangHoa> HangHoa = context.HangHoas
				.Include(x => x.ImgHangHoas)
				.Where(x => x.Idnhh == nhomHangHoa.Idnhh && x.HienThi == true && x.Active == true)
				.ToList();

            ViewBag.HangHoa = HangHoa;


			ViewBag.HangHoa = context.HangHoas
	   .Include(x => x.ImgHangHoas)
	   .Where(x => x.HienThi == true && x.Active == true).ToList();


			ViewBag.NhomHangHoa = context.NhomHangHoas.Where(x => x.Active == true && x.HienThi == true).ToList();
			ViewBag.Voucher = context.Vouchers.Where(x => x.Active == true && x.HienThi == true).ToList();

			ViewBag.NhomHangHoaCap1 = context.NhomHangHoas.Where(x => x.Active == true && x.Levels == 1 && x.HienThi == true).ToList();
			ViewBag.NhomHangHoaCap2 = context.NhomHangHoas.Where(x => x.Active == true && x.Levels == 2 && x.HienThi == true).ToList();
			return View("NhomSP", nhomHangHoa);
		}


	}
}
