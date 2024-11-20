using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNS.Models;
using PagedList;

namespace QLNS.Controllers
{
    public class CongTacController : Controller
    {
        QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext();
        // GET: CongTac

        public ActionResult RedirectToCongTac()
        {
            return RedirectToAction("CongTac", "CongTac");
        }
        public ActionResult CongTac()
        {
            var congtac = from ct in db.CongTacs
                          join nv in db.NhanViens on ct.MaNV equals nv.MaNV
                          select new CongTacModel
                          {
                              MaCT = ct.MaCT,
                              HoTen = nv.HoTen,
                              NgayBatDau = ct.NgayBatDau.GetValueOrDefault(),
                              NgayKetThuc = ct.NgayKetThuc.GetValueOrDefault(),
                              DiaDiem = ct.DiaDiem,
                              MucDich = ct.MucDich,
                              BieuMau = ct.BieuMau,
                              TrangThai = ct.TrangThai
                          };
            var congtacList = congtac.ToList();
            return View(congtacList);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemCongTac(CongTac item)
        {
            db.CongTacs.InsertOnSubmit(item);
            db.SubmitChanges();
            return RedirectToAction("CongTac", "CongTac");
        }


        public ActionResult SuaCongTac(int id)
        {
            CongTac item = db.CongTacs.SingleOrDefault(n => n.MaCT == id);
            return View(item);
        }
        [HttpPost]
        public ActionResult SuaCongTac(CongTac ct)
        {
            CongTac itemm = db.CongTacs.SingleOrDefault(n => n.MaCT == ct.MaCT);
            itemm.MaNV = ct.MaNV;
            itemm.NgayBatDau = ct.NgayBatDau;
            itemm.NgayKetThuc = ct.NgayKetThuc;
            itemm.MucDich = ct.MucDich;
            itemm.DiaDiem = ct.DiaDiem;
            itemm.BieuMau = ct.BieuMau;
            itemm.TrangThai = ct.TrangThai;
            db.SubmitChanges();
            return RedirectToAction("CongTac");
        }

        //public ActionResult DanhSachCongTac(int page = 1, int pageSize = 10)
        //{
        //    // Lấy danh sách công tác kết hợp với thông tin nhân viên
        //    var congtac = from ct in db.CongTacs
        //                  join nv in db.NhanViens on ct.MaNV equals nv.MaNV
        //                  select new CongTacModel
        //                  {
        //                      MaCT = ct.MaCT,
        //                      HoTen = nv.HoTen,
        //                      NgayBatDau = ct.NgayBatDau.GetValueOrDefault(),
        //                      NgayKetThuc = ct.NgayKetThuc.GetValueOrDefault(),
        //                      DiaDiem = ct.DiaDiem,
        //                      MucDich = ct.MucDich,
        //                      BieuMau = ct.BieuMau,
        //                      TrangThai = ct.TrangThai
        //                  };

        //    // Tổng số bản ghi
        //    int totalCongTac = congtac.Count();

        //    // Tính tổng số trang
        //    int totalPages = (int)Math.Ceiling((double)totalCongTac / pageSize);

        //    // Giới hạn page trong phạm vi hợp lệ
        //    page = page < 1 ? 1 : page > totalPages ? totalPages : page;

        //    // Lấy dữ liệu cho trang hiện tại
        //    var congtacList = congtac
        //        .OrderBy(ct => ct.MaCT) // Sắp xếp theo mã công tác
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    // Truyền dữ liệu phân trang qua ViewBag để sử dụng trong View
        //    ViewBag.PageNumber = page;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalPages = totalPages;

        //    return View(congtacList);
        //}

        public ActionResult DanhSachCongTac(int page = 1, int pageSize = 10)
        {
            var congtac = from ct in db.CongTacs
                          join nv in db.NhanViens on ct.MaNV equals nv.MaNV
                          select new CongTacModel
                          {
                              MaCT = ct.MaCT,
                              HoTen = nv.HoTen,
                              NgayBatDau = ct.NgayBatDau.GetValueOrDefault(),
                              NgayKetThuc = ct.NgayKetThuc.GetValueOrDefault(),
                              DiaDiem = ct.DiaDiem,
                              MucDich = ct.MucDich,
                              BieuMau = ct.BieuMau,
                              TrangThai = ct.TrangThai
                          };

            var congTacs = congtac.OrderBy(c => c.MaCT)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            int totalItems = congtac.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.PageNumber = page;
            ViewBag.TotalPages = totalPages;

            return View(congTacs);
        }




    }
}