using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OctOcean.Entity;
using OctOcean.WebSite.Models;

namespace OctOcean.WebSite.Controllers
{
    [Route("Article")]
    public class ArticleController : Controller
    {
        OctOcean.DataService.Pub_Article_Dal dal = new OctOcean.DataService.Pub_Article_Dal();
        
        public IActionResult Index()
        {
            return View();
            ////Pri_ArticleDraft_Dal dal = new Pri_ArticleDraft_Dal();
            //var all= dal.GetAllPri_ArticleDraft();
            //return View(all);
        }

        [HttpGet("Detail/{ArticleKey}")] //http://localhost:6041/Article/Detail/sdf
        public IActionResult Detail(string ArticleKey)
        {
            var entity= dal.GetPub_Article_Entity(ArticleKey);
            if (entity == null)
            {
                //如果没有找到就回到列表中来
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = entity.ArticleTitle;

            ArticleDetail_M detail_M = new ArticleDetail_M()
            {
                ContentText = entity.ContentText,
                LastUpdate = entity.UpdateTime.ToShortDateString(),
                Title = entity.ArticleTitle
            };



            return View(detail_M);

        }
    }
}