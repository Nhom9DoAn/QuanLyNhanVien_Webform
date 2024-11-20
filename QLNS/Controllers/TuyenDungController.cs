using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNS.Models;

namespace QLNS.Controllers
{
    public class TuyenDungController : Controller
    {
        QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext();
        // GET: CongViec
        public ActionResult Index()
        {
            var listCongViec = db.TuyenDungs.ToList();
            return View(listCongViec);
        }
    }
}