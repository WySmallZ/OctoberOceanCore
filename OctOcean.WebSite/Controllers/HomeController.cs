using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OctOcean.WebSite.DataService;

namespace OctOcean.WebSite.Controllers
{
    public class HomeController : Controller
    {
        //private readonly OctOceanContext _context;
        //public HomeController(OctOceanContext context)
        //{
        //    this._context = context;
        //}
        public IActionResult Index()
        {

            //var all = _context.Pub_Article_Entity_DbSet.ToList();
            var s= Utils.ConfigHelper.DefaultConnectionString;
            return View();

        }
    }
}