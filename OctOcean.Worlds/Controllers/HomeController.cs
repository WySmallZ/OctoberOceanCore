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
        [Route("{PageIndex:int?}")]
        [Route("Home/{ArticleCategory?}/{PageIndex:int?}")]
        public IActionResult Index(string ArticleCategory,int PageIndex=1)
        {
            int sumcount = 0;
            List<Entity.Aux_HomeArticlePager_Entity> data = dal.GetAllNotDel_Pub_Article_Entity(ArticleCategory, out sumcount, PageIndex, PageSize: 10);
            if (data == null)
            {
                //如果没没有数据重新从第一页加载
                data = dal.GetAllNotDel_Pub_Article_Entity("", out sumcount, PageIndex:1, PageSize: 10);
                ViewBag.CurrentArticleCategory = "Home";
            }
            else
            {
                ViewBag.CurrentArticleCategory = string.IsNullOrWhiteSpace(ArticleCategory) ? "Home" : ArticleCategory;
            }



            //查询出菜单项
            Models.ArticleList_VM vm = new ArticleList_VM();
            vm.Article_EntityArray = data.ToArray();
            vm.ArticleSumCount = sumcount;
            vm.CurrentPageIdex = PageIndex;


            return View(vm);
        }




    }
}
