using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OctOcean.Worlds.Models;

namespace OctOcean.Worlds.Controllers
{
    public class HomeController : Controller
    {
        OctOcean.DataService.Pub_Article_Dal dal = new OctOcean.DataService.Pub_Article_Dal();
        //private readonly OctOceanContext _context;
        //public HomeController(OctOceanContext context)
        //{
        //} 
        public IActionResult Index()
        { 
            return View(dal.GetAllPub_Article_Entity());
        }

        
    }
}
