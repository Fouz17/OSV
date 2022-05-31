﻿using OSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSV.Controllers
{
    public class HomeController : Controller
    {
        private readonly DB _DB = new DB();
        public ActionResult Index()
        {
            var x = _DB.tblOMRDatas.First();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}