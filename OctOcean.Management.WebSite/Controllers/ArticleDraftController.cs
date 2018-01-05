using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OctOcean.Management.WebSite.Controllers
{
    public class ArticleDraftController : Controller
    {
        private IConfiguration _iconfiguration;

        public ArticleDraftController(IConfiguration configuration)
        {
            this._iconfiguration = configuration;
        }
        public IActionResult Index()
        {
            string con=_iconfiguration.GetConnectionString("defaultConnStr");
            return View();
        }

        
    }
}