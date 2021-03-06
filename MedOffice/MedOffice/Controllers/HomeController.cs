﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedOffice.Models;
using Microsoft.AspNet.Identity.Owin;

namespace MedOffice.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult DoctorIndex()
        {
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