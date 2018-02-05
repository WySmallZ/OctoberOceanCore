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
            string _AidStyle = string.Empty;
            string _ArticleDesc = string.Empty;

            int _BrowseCount = dal.GetPub_Article_BrowseCount(ArticleKey);

            if ("p".Equals(t, StringComparison.InvariantCultureIgnoreCase))
            {
                //执行预览
                var draftentity = new OctOcean.DataService.Pri_ArticleDraft_Dal().GetPri_ArticleDraft(ArticleKey);
                _ContentText = draftentity.ContentText;
                _LastUpdate = draftentity.UpdateTime.ToString("yyyy-MM-dd");
                _Title = draftentity.ArticleTitle;
                _AidStyle = draftentity.AidStyle;
                _ArticleDesc = draftentity.ArticleDesc;

            }
            else
            {
                var entity = dal.Get_NotDel_Pub_Article_Entity(ArticleKey);

                if (entity == null)
                {
                    //如果没有找到就回到列表中来
                    return RedirectToAction("Index", "Home");
                }


                _ContentText = entity.ContentText;
                _LastUpdate = entity.UpdateTime.ToString("yyyy-MM-dd");
                _Title = entity.ArticleTitle;
                _AidStyle = entity.AidStyle;
                _ArticleDesc = entity.ArticleDesc;

            }



            ArticleDetail_VM detail_M = new ArticleDetail_VM()
            {
                ContentText = _ContentText,
                BrowseCount = _BrowseCount,
                LastUpdate = _LastUpdate,
                Title = _Title,
                AidStyle = _AidStyle,
                ArticleDesc = _ArticleDesc



            };

            //插入浏览日志
            new DataService.Pub_AccessRecord_Dal().InsertAccessRecord(new Entity.Pub_AccessRecord_Entity()
            {
                AccessUrl = Helper.PageControllerHelper.GetAbsoluteUri(Request),
                IP = Helper.PageControllerHelper.GetClientUserIp(HttpContext),
                PageTag = "ArticleDetail",
                SessionID = HttpContext.Session.Id
            }, new Entity.Pub_ArticleBrowseLog_Entity()
            {
                AccessUrl = Helper.PageControllerHelper.GetAbsoluteUri(Request),
                IP = Helper.PageControllerHelper.GetClientUserIp(HttpContext),
                ArticleKey = ArticleKey,
                SessionID = HttpContext.Session.Id
            });




            ViewData["Title"] = _Title;
            return View(detail_M);

        }
    }
}