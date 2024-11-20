﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNS.Models;

namespace QLNS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext();

        public ActionResult Index()
        {
            var listCongViec = db.TuyenDungs.ToList();
            return View(listCongViec);
            //return View();
        }


    }
}