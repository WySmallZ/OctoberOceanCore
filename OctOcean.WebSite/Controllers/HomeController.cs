﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OctOcean.WebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var s= Utils.ConfigHelper.DefaultConnectionString;
            return View();

        }
    }
}