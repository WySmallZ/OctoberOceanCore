using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OctOcean.Worlds.Models;

namespace OctOcean.Worlds.Controllers
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
        public IActionResult Detail(string ArticleKey, string t = "")
        {
            string _ContentText = string.Empty;
            string _LastUpdate = string.Empty;
            string _Title = string.Empty;

            if ("p".Equals(t, StringComparison.InvariantCultureIgnoreCase))
            {
                //执行预览
                var draftentity = new OctOcean.DataService.Pri_ArticleDraft_Dal().GetPri_ArticleDraft(ArticleKey);
                _ContentText = draftentity.ContentText;
                _LastUpdate = draftentity.UpdateTime.ToShortDateString();
                _Title = draftentity.ArticleTitle;

            }
            else
            {
                var entity= dal.GetPub_Article_Entity(ArticleKey);

                if (entity == null)
                {
                    //如果没有找到就回到列表中来
                    return RedirectToAction("Index", "Home");
                }


                _ContentText = entity.ContentText;
                _LastUpdate = entity.UpdateTime.ToShortDateString();
                _Title = entity.ArticleTitle;

            }



            ArticleDetail_VM detail_M = new ArticleDetail_VM()
            {
                ContentText = _ContentText,
                BrowseCount = 100,
                LastUpdate = _LastUpdate,
                Title = _Title
               
            };


            ViewData["Title"] = _Title;
            return View(detail_M);

        }
    }
}