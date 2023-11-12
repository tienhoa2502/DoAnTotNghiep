using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using System;
using System.Linq;
using System.Threading.Tasks;
//using X.PagedList; // Make sure to install the X.PagedList.Mvc.Core NuGet package for pagination support
//using WebBanHangOnline.Models;
//using WebBanHangOnline.Models.EF;


namespace BanHangOnl.Controllers
{
    public class TinTucBlogController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
		

		private readonly QuanLyBanHangContext _context;
		//public TinTucBlogController(QuanLyBanHangContext context)
		//{
		//	_context = context;
		//}

		public ActionResult Index()
        {
            var items = context.TinTucs.ToList();

            return View();
        }


	}
}

