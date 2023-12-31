﻿using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BanHangOnl.Models;
using BanHangOnl.Models.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using QuanLyNhaHang.Models.Mapping;
using System.Data;
using System.Text.RegularExpressions;

namespace BanHangOnl.Areas.Admin.Controllers
{

    [Area("Admin")]
	[Authorize(Roles = "NhanVien, QuanLy")]
	public class PhieuXuatController : Controller
    {
        QuanLyBanHangContext context = new QuanLyBanHangContext();
        private readonly IMapper _mapper;
        public PhieuXuatController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("/DonDatHang")]
        public IActionResult Index()
        {
            ViewBag.PhieuXuat = context.PhieuXuats
                .Include(x => x.ChiTietPhieuXuats)
                .Include(x => x.IdnvNavigation)
                .Include(x => x.IdtkNavigation)
                    .ThenInclude(tk => tk.KhachHangs)
                .Where(x => x.Active == true).ToList();




            return View();
        }
        [HttpPost("/DonDatHang/getDonViTinhVaSL")]
        public async Task<dynamic> getDonViTinhVaSL(int idHH, int idMau, int idSize)
        {
            var tonKho = context.ChiTietPhieuNhaps
                .Include(x => x.IdhhNavigation)
                .Where(x => x.Idhh == idHH && x.Idmau == idMau && x.Idsize == idSize)
                .ToList();
            double slc = 0;
            if (tonKho.Count() != 0)
            {
                double sl = (double)tonKho.Sum(x => x.SoLuong);
                double slx = (double)tonKho.Sum(x => x.SoLuongXuat);
                slc = sl - slx;
            }

            var ton = new
            {
                DonViTinh = getDonViTinh(tonKho.Count() != 0 ?(int)tonKho.First().Idhh : idHH).Result,
                SoLuong = slc,
                DonGia = tonKho.Count() != 0 ? tonKho?.First().IdhhNavigation.GiaBan : 0,
            };

            return ton;
        }
        static string GetPhoneNumber(string input)
        {
            // Biểu thức chính quy để tìm số điện thoại
            // Điều này giả sử số điện thoại có định dạng 10 chữ số và bắt đầu bằng "0"
            string pattern = @"^0\d{9}$";

            // Tìm kiếm phù hợp
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                // Trả về giá trị phù hợp (số điện thoại)
                return match.Value;
            }
            else
            {
                // Nếu không tìm thấy số điện thoại
                return "Không tìm thấy số điện thoại.";
            }
        }
        [HttpPost("/DonDatHang/GetThongTinKhachHang")]
        public dynamic getThongTin(string sdt)
        {
            KhachHang khachHang = context.KhachHangs.FirstOrDefault(x => x.Phone == sdt);

            return khachHang;   
        }
        [HttpPost("/DonDatHang/ThemDonDatHang")]
        public async Task<dynamic> luuPhieuXuat([FromBody] TTPhieuXuat data)
        {
            PhieuXuatMap ph = data.DonDatHang;
            KhachHangMap kh = data.KhachHang;
            KhachHang khachHang = _mapper.Map<KhachHang>(kh);
            string cleanedInput = Regex.Replace(khachHang.Phone, @"\s+", "");

            // Sử dụng biểu thức chính quy để tìm số điện thoại
            khachHang.Phone = cleanedInput; 
            PhieuXuat phieuXuat = _mapper.Map<PhieuXuat>(ph);
            List<ChiTietPhieuXuatMap> chiTietPhieuXuatMaps = data.ChiTietDonDatHang;
            List<ChiTietPhieuNhap> soLuongHhcon = await context.ChiTietPhieuNhaps
                .Include(x => x.IdpnNavigation)
                .Include(x => x.IdhhNavigation)
                .OrderBy(x => x.IdpnNavigation.NgayNhap).ToListAsync();
            var tran = context.Database.BeginTransaction();
            try
            {
                TaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(x => x.TenTk == khachHang.Phone);
                List<KhachHang> a = context.KhachHangs.ToList();
                if (taiKhoan == null)
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk.TenTk = khachHang.Phone;
                    tk.Pass = khachHang.Phone;
                    tk.Active = true;
                    tk.Idvt = 7;
                    context.TaiKhoans.Add(tk);
                    context.SaveChanges();
                    taiKhoan = tk;
                    khachHang.MaKh = "KH" + (a.Count() + 1).ToString("D4"); 
                    khachHang.Idtk = tk.Idtk;
                    khachHang.Active = true;
                    context.KhachHangs.Add(khachHang);
                    context.SaveChanges();
                }
                phieuXuat.Idtk = taiKhoan.Idtk;
                phieuXuat.NgayTao = DateTime.Now;
                phieuXuat.Active = true;
                phieuXuat.Idnv = 1;
                phieuXuat.SoPx = taoSoPhieuXuat(context);
                context.PhieuXuats.Add(phieuXuat);
                context.SaveChanges();

                foreach (ChiTietPhieuXuatMap t in chiTietPhieuXuatMaps.ToList())
                {
                    double slq = double.Parse(t.SoLuong);
                    foreach (ChiTietPhieuNhap slhhc in soLuongHhcon.Where(x => x.Idhh == int.Parse(t.Idhh) && x.Idmau == int.Parse(t.Idmau) && x.Idsize == int.Parse(t.Idsize) && x.SoLuong != x.SoLuongXuat))
                    {
                        slhhc.SoLuongXuat = slhhc.SoLuongXuat ?? 0;
                        double slc = (double)((double)slhhc.SoLuong - slhhc.SoLuongXuat);
                        ChiTietPhieuXuat ct = new ChiTietPhieuXuat();
                        ct.Idhh = int.Parse(t.Idhh);
                        ct.Idpx = phieuXuat.Idpx;
                        ct.Gia = double.Parse(t.Gia);
                        ct.Idctpn = slhhc.Idctpn;
                        ct.Idmau = int.Parse(t.Idmau);
                        ct.Idsize = int.Parse(t.Idsize);
                        ct.Active = true;
                        //nếu mà trong kho còn nhiều hơn số xuất
                        if (slc > slq)
                        {
                            ct.SoLuong = double.Parse(t.SoLuong);
                            slhhc.SoLuongXuat += Convert.ToInt32(slq);
                            context.ChiTietPhieuXuats.Add(ct);
                            context.SaveChanges();
                            break;
                        }
                        //nếu mà trong kho ngang với số cần xuất
                        if (slc == slq)
                        {
                            ct.SoLuong = slc;
                            slhhc.SoLuongXuat += Convert.ToInt32(slq);
                            context.ChiTietPhieuXuats.Add(ct);
                            context.SaveChanges();
                            break;
                        }
                        //nếu trong kho còn ít hơn số cần xuất
                        if (slc < slq)
                        {
                            ct.SoLuong = (double)slc;
                            slq = (double)(slq - slc);
                            slhhc.SoLuongXuat += Convert.ToInt32(slc);
                            t.SoLuong = slq.ToString();
                            context.ChiTietPhieuXuats.Add(ct);
                            context.SaveChanges();
                        }
                        context.ChiTietPhieuNhaps.Update(slhhc);
                        context.SaveChanges();
                    }
                    chiTietPhieuXuatMaps.Remove(t);
                    context.SaveChanges();
                }
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
        public static string taoSoPhieuXuat(QuanLyBanHangContext context)
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("yyyyMMdd");
            var phieuNhap = context.PhieuXuats.Where(x => x.SoPx.Contains(date)).ToList();
            return $"PX-{date}-{(phieuNhap.Count() + 1).ToString("D2")}";
        }
        [HttpGet("/DonDatHang/ChiTietDonDatHang/{id}")]
        public IActionResult Info(int id)
        {
            ViewBag.DDH = context.PhieuXuats
                .Include(x => x.ChiTietPhieuXuats)
                .ThenInclude(x => x.IdhhNavigation)
                    .Include(x => x.ChiTietPhieuXuats)
                        .ThenInclude(x => x.IdmauNavigation)
                    .Include(x => x.ChiTietPhieuXuats)
                        .ThenInclude(x => x.IdsizeNavigation)
                .Include(x => x.IdnvNavigation)
                .FirstOrDefault(x => x.Idpx == id);
            return View();
        }
        [HttpPost("/DonDatHang/TraHang")]
        public dynamic traHang(int idPX)           
        {
            try
            {
                PhieuXuat px = context.PhieuXuats.Find(idPX);
                List<ChiTietPhieuXuat> chiTietPhieuXuats = context.ChiTietPhieuXuats.Where(x => x.Idpx == idPX).ToList();
                foreach (ChiTietPhieuXuat chiTietPhieu in chiTietPhieuXuats)
                {
                    ChiTietPhieuNhap chiTietPhieuNhap = context.ChiTietPhieuNhaps.Include(x => x.IdpnNavigation).FirstOrDefault(x => x.Idctpn == chiTietPhieu.Idctpn);
                    chiTietPhieuNhap.SoLuongXuat -= Convert.ToInt32(chiTietPhieu.SoLuong);
                    context.ChiTietPhieuNhaps.Update(chiTietPhieuNhap);
                    context.SaveChanges();
                    
                }
                px.DonTra = true;
                context.PhieuXuats.Update(px);
                context.SaveChanges();
                return new
                {
                    statusCode = 200,
                    message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    statusCode = 500,
                    message = "Thất bại"
                };
            }
        }

        [HttpGet("/DonDatHang/Them")]
        public IActionResult viewAdd()
        {
            //ViewBag.NhaCungCap = context.NhaCungCaps.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();
            ViewBag.NhanVien = context.NhanViens.Where(x => x.Active == true /*&& x.Levels == 2*/).ToList();
            return View("AddPhieuXuat");
        }
        [HttpPost("/DonDatHang/getDonViTinh")]
        public async Task<dynamic> getDonViTinh(int idHH)
        {
            HangHoa dvt = await context.HangHoas
                .Include(x => x.IddvtNavigation)
                .FirstOrDefaultAsync(x => x.Idhh == idHH);
            return dvt?.IddvtNavigation?.TenDvt;
        }

        [Route("/DonDatHang/Them")]

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

        [Route("/DonDatHang/ViewSua/{id}")]
        public IActionResult viewEdit(int id)
        {
            PhieuXuat sua = context.PhieuXuats.Find(id);
            return View("Edit", sua);
        }

        [Route("/DonDatHang/Sua")]

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

        [Route("/DonDatHang/Xoa/{id}")]
        public IActionResult Xoa(int id)
        {
            PhieuNhap tt = context.PhieuNhaps.Find(id);
            tt.Active = false;

            context.PhieuNhaps.Update(tt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public static string taoSoPhieuNhap(QuanLyBanHangContext context)
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
    public class TTPhieuXuat
    {
        public KhachHangMap KhachHang { get; set; }
        public PhieuXuatMap DonDatHang { get; set; }
        public List<ChiTietPhieuXuatMap> ChiTietDonDatHang { get; set; }
    }
}
