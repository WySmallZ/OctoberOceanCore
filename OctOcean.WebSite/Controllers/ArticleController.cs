using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OctOcean.Entity;
namespace OctOcean.WebSite.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
            ////Pri_ArticleDraft_Dal dal = new Pri_ArticleDraft_Dal();
            //var all= dal.GetAllPri_ArticleDraft();
            //return View(all);
        }
    }
}