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
        [Route("")]
        [Route("Home/{ArticleCategory?}")]
        public IActionResult Index(string ArticleCategory)
        {
            //查询出菜单项
            Models.ArticleList_VM vm = new ArticleList_VM();

            List<Entity.Pub_Article_Entity> data = null;
            //如果文章类别不为空
            if (!string.IsNullOrWhiteSpace(ArticleCategory))
            {
                data = dal.GetAllNotDel_Pub_Article_EntityByArticleCategory(ArticleCategory);
            }
            if (data == null)
            {
                data = dal.GetAllNotDel_Pub_Article_Entity();
                ViewBag.CurrentArticleCategory = "Home";
            }
            else
            {
                ViewBag.CurrentArticleCategory = ArticleCategory;
            }

            vm.Article_EntityArray = data.ToArray();


            return View(vm);
        }




    }
}
