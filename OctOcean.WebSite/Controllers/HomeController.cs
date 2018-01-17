using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OctOcean.WebSite.Controllers
{
    public class HomeController : Controller
    {
        OctOcean.DataService.Pub_Article_Dal dal = new OctOcean.DataService.Pub_Article_Dal();
        //private readonly OctOceanContext _context;
        //public HomeController(OctOceanContext context)
        //{
        //    this._context = context;
        //}
        public IActionResult Index()
        {
            return View(dal.GetAllPub_Article_Entity());

           // //默认显示最新的文章
           // //var all = _context.Pub_Article_Entity_DbSet.ToList();
           //// var s= Utils.OctOceanConfig.DefaultConnectionString;
           // return View();

        }
    }
}