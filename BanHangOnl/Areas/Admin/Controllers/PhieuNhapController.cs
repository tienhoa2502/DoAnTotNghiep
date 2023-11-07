using AutoMapper;
using BanHangOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Models.Mapping;

namespace BanHangOnl.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class PhieuNhapController : Controller
    {
        QuanLyNhaHangContext context = new QuanLyNhaHangContext();
        private readonly IMapper _mapper;
        public PhieuNhapController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("/PhieuNhap")]
        public IActionResult Index()
        {
            ViewBag.PhieuNhap = context.PhieuNhaps
                .Include(x => x.ChiTietPhieuNhaps)
                .Include(x => x.IdnvNavigation).Where(x => x.Active == true).ToList();
            return View();
        }
        [HttpGet("/PhieuNhap/Xem")]
        public IActionResult Info()
        {

            return View();
        }

        [HttpGet("/PhieuNhap/Them")]
        public IActionResult viewAdd()
        {
            ViewBag.NhaCungCap = context.NhaCungCaps.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();
            ViewBag.NhanVien = context.NhanViens.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();
            return View("AddPhieuNhap");
        }
        [HttpPost("/NhapKho/getDonViTinh")]
        public async Task<dynamic> getDonViTinh(int idHH)
        {
            HangHoa dvt = await context.HangHoas
                .Include(x => x.IddvtNavigation)
                .FirstOrDefaultAsync(x => x.Idhh == idHH);
            return dvt.IddvtNavigation.TenDvt;
        }
        [HttpPost("/NhapKho/ThemPhieuNhap")]
        public async Task<dynamic> ThemPhieuNhap([FromBody] TTPhieuNhap data)
        {
            //NhanVien nv = context.NhanViens.Find(Int32.Parse(User.Identity.Name));
            PhieuNhapMap phieuNhapMap = data.PhieuNhap;
            List<ChiTietPhieuNhapMap> chiTietPhieuNhapMaps = data.ChiTietPhieuNhap;


            using var tran = context.Database.BeginTransaction();
            try
            {
                List<ChiTietPhieuNhap> chiTietPhieuNhaps = _mapper.Map<List<ChiTietPhieuNhap>>(chiTietPhieuNhapMaps);
                PhieuNhap phieuNhap = _mapper.Map<PhieuNhap>(phieuNhapMap);

                phieuNhap.SoPn = taoSoPhieuNhap(context);
                phieuNhap.Idnv = 1;
                phieuNhap.Active = true;
                await context.PhieuNhaps.AddAsync(phieuNhap);
                await context.SaveChangesAsync();
                foreach (ChiTietPhieuNhap chiTiet in chiTietPhieuNhaps)
                {
                    chiTiet.Idpn = phieuNhap.Idpn;
                    chiTiet.Active = true;
                }
                await context.ChiTietPhieuNhaps.AddRangeAsync(chiTietPhieuNhaps);
                await context.SaveChangesAsync();
                List<TonKho> tonKhos = new List<TonKho>();
                foreach (ChiTietPhieuNhap chiTiet1 in chiTietPhieuNhaps)
                {
                    TonKho tonKho = new TonKho();
                    tonKho.Idctpn = chiTiet1.Idctpn;
                    tonKho.SoLuong = chiTiet1.SoLuong;
                    tonKho.NgayNhap = phieuNhap.NgayNhap;
                    tonKhos.Add(tonKho);
                }
                await context.TonKhos.AddRangeAsync(tonKhos);
                await context.SaveChangesAsync();
                tran.Commit();
                return new
                {
                    statusCode = 200,
                    message = "Thành công",
                };
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return new
                {
                    statusCode = 500,
                    message = "Thất bại",
                };
            }
        }
        [Route("/PhieuNhap/Them")]

        public IActionResult Add(PhieuNhap vaiTro)
        {

            ViewBag.PhieuNhap = context.PhieuNhaps
                .Include(x => x.ChiTietPhieuNhaps)
                .Include(x => x.IdnvNavigation).Where(x => x.Active == true).ToList();

            ViewBag.CHiTietPhieuNhap = context.ChiTietPhieuNhaps.Where(x => x.Active == true).ToList();

            ViewBag.NhanVien = context.NhanViens.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();

            vaiTro.Active = true;
            vaiTro.NgayNhap = DateTime.Now;
            //vaiTro.NgaySua = DateTime.Now;
            context.PhieuNhaps.Add(vaiTro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/PhieuNhap/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            PhieuNhap sua = context.PhieuNhaps.Find(id);
            return View("Edit", sua);
        }

        [Route("/PhieuNhap/Sua")]

        public IActionResult Edit(PhieuNhap vaiTro)
        {
            PhieuNhap tt = context.PhieuNhaps.Find(vaiTro.Idpn);
            //tt.Img = vaiTro.Img;
            //tt.TenTt = vaiTro.TenTt;
            //tt.ChiTiet = vaiTro.ChiTiet;

            

            context.PhieuNhaps.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/PhieuNhap/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            PhieuNhap tt = context.PhieuNhaps.Find(id);
            tt.Active = false;

            context.PhieuNhaps.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public static string taoSoPhieuNhap(QuanLyNhaHangContext context)
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("yyyyMMdd");
            var phieuNhap = context.PhieuNhaps.Where(x => x.SoPn.Contains(date)).ToList();
            return $"PN-{date}-{(phieuNhap.Count() + 1).ToString("D2")}";
        }
        public class TTPhieuNhap
        {
            public PhieuNhapMap PhieuNhap { get; set; }
            public List<ChiTietPhieuNhapMap> ChiTietPhieuNhap { get; set; }
        }
    }
}
