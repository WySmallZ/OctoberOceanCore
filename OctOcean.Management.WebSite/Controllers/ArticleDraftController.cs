using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OctOcean.Entity;

namespace OctOcean.Management.WebSite.Controllers
{
    public class ArticleDraftController : Controller
    {

       
            OctOcean.DataService.Pri_ArticleDraft_Dal draftdal = new DataService.Pri_ArticleDraft_Dal();

        private IConfiguration _iconfiguration;

        public ArticleDraftController(IConfiguration configuration)
        {
            this._iconfiguration = configuration;
        }
        public IActionResult Index()
        {
            string con = _iconfiguration.GetConnectionString("defaultConnStr");
            return View();
        }



        public object Save(string ArticleKey, string ArticleTitle, string ArticleCategory, string ContentText, string ArticleTag, string AidStyle)
        {
            int _status = 0;
            string _msg = string.Empty;
            int savecount = 0;
            //只用判断标题、样式、和内容
            if (ContentText != null)
            {
                var _cont = ContentText.Replace("<p>", "").Replace("<br>", "").Replace("</p>", "").Replace("</br>", ""); //去掉自带的样式
                if (string.IsNullOrWhiteSpace(_cont))
                {
                    ContentText = "";
                }

            }


            if (string.IsNullOrWhiteSpace(ArticleTitle) && string.IsNullOrWhiteSpace(ContentText) && string.IsNullOrWhiteSpace(AidStyle))
            {
                _status = 3;
                _msg = "未提交数据";
            }
            else
            {
                //将数据存入到Temp中去
                OctOcean.DataService.Pri_ArticleDraft_Temp_Dal tempdal = new DataService.Pri_ArticleDraft_Temp_Dal();
                try
                {
                    tempdal.InsertPri_ArticleDraft_Temp(new Entity.Pri_ArticleDraft_Temp_Entity()
                    {
                        ArticleKey = ArticleKey,
                        ArticleTag = ArticleTag ?? "",
                        AidStyle = AidStyle ?? "",
                        ArticleCategory = ArticleCategory ?? "",
                        ArticleTitle = ArticleTitle ?? "",
                        ContentText = ContentText ?? "",
                        DelStatus = 0,
                        UpdateTime = DateTime.Now

                    });

                    //直接更新到Draft中去

                    //必须在Draft中有这条记录修改才能生效，也就是说，只有点了保存按钮，产生了数据，才能够和temp数据进行关联，这样的话可以减少草稿内容（比如测试、或者打开页面没有做任何事情）的产生。
                    draftdal.UpdatePri_ArticleDraft(new Entity.Pri_ArticleDraft_Entity()
                    {
                        ArticleKey = ArticleKey,
                        ArticleTag = ArticleTag ?? "",
                        AidStyle = AidStyle ?? "",
                        ArticleCategory = ArticleCategory ?? "",
                        ArticleTitle = ArticleTitle ?? "",
                        ContentText = ContentText ?? "",
                        DelStatus = 0,
                        UpdateTime = DateTime.Now
                    });

                    //查询历史总次数
                    savecount = tempdal.GetSaveTempCountByArticleKey(ArticleKey);
                    _status = 1;
                }
                catch (Exception ex)
                {
                    _status = 4;
                    _msg = ex.Message;
                }

            }
            return new { status = _status, msg = _msg, ak = ArticleKey, sc = savecount };

        }



        public object Publish(string ArticleKey,bool IsPublish)
        {
            string _msg = string.Empty;
            int _status = 0;
            //先判断是否已经存在了Draft中去
            if(draftdal.GetPri_ArticleDraft(ArticleKey)==null)
            {
                _status = 2;
                _msg = "请先保存数据";

            }
            else
            {
                OctOcean.DataService.Pub_Article_Dal padal = new DataService.Pub_Article_Dal();
                try
                {
                    padal.InsertPub_ArticleWithPri_ArticleDraft(ArticleKey,IsPublish);
                    _status = 1;
                }
                catch (Exception ex )
                {
                    _status = 4;
                    _msg = ex.Message;
                }
               
            }
            return new { status = _status, msg = _msg };

        }



        [Route("ArticleDraft/Pagination/{PageIndex}/{PageSize}/{ArticleCategoryCode?}")]
        public object Pager(int PageIndex,int PageSize,string ArticleCategoryCode)
        {
            int sumcount = 0;
            IList<Pri_ArticleDraftPager_Entity> data = null;
            if(string.IsNullOrEmpty(ArticleCategoryCode))
            {

                 data= draftdal.GetPri_ArticleDraftPagerList("", PageIndex, PageSize, null,out sumcount);
                
            }
            else
            {
                data= draftdal.GetPri_ArticleDraftPagerList(" AND d.ArticleCategory=@ArticleCategory ", PageIndex, PageSize, new { ArticleCategory= ArticleCategoryCode },out sumcount);
            }
            return new { SumCount = sumcount, Data = data };

        }
    }
}