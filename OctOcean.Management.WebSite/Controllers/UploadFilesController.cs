using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OctOcean.Management.WebSite.Controllers
{
    [Route("Upload")]
    public class UploadFilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult wy(string name)
        {
            return Content("Post");
        }
    }
}