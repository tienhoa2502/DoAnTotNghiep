﻿using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TonKhoController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        [HttpGet("/TonKho")]
        public IActionResult Index()
        {
            ViewBag.NhomTinTuc = context.NhomTinTucs.Where(x => x.Active == true).ToList();

            return View();
        }
    }
}
