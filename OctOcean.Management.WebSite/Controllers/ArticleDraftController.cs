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
        //public IActionResult Index()
        //{
        //    string con = _iconfiguration.GetConnectionString("defaultConnStr");
        //    return View();
        //}



        /// <summary>
        /// 定时自动保存文章到Draft和Temp中去
        /// </summary>
        /// <param name="ArticleKey"></param>
        /// <param name="ArticleTitle"></param>
        /// <param name="ArticleCategory"></param>
        /// <param name="ContentText"></param>
        /// <param name="ArticleTag"></param>
        /// <param name="ArticleDesc"></param>
        /// <param name="AidStyle"></param>
        /// <returns></returns>
        public object Save(string ArticleKey, string ArticleTitle, string ArticleCategory, string ContentText, string ArticleTag,string ArticleDesc, string AidStyle)
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


            if (string.IsNullOrWhiteSpace(ArticleTitle) && string.IsNullOrWhiteSpace(ContentText)&&string.IsNullOrWhiteSpace(ArticleDesc) && string.IsNullOrWhiteSpace(AidStyle))
            {
                _status = 3;
                _msg = "未提交数据";
            }
            else
            {
           ContentText = new DataService.Pri_ArticleImage_Dal().ReplaceImagesPlaceholder(ContentText, ArticleKey);

                //将数据存入到Temp中去
                OctOcean.DataService.Pri_ArticleDraft_Temp_Dal tempdal = new DataService.Pri_ArticleDraft_Temp_Dal();
                try
                {
                   int oi= tempdal.InsertPri_ArticleDraft_Temp(new Entity.Pri_ArticleDraft_Temp_Entity()
                    {
                        ArticleKey = ArticleKey,
                        ArticleTag = ArticleTag ?? "",
                         ArticleDesc= ArticleDesc??"",
                        AidStyle = AidStyle ?? "",
                        ArticleCategory = ArticleCategory ?? "",
                        ArticleTitle = ArticleTitle ?? "",
                        ContentText = ContentText ?? "",
                        UpdateTime = DateTime.Now

                    });
                    //如果有改动的话，就进行同步修改，注意：仍然要保存旧的删除的状态
                    if (oi > 0)
                    {
                        //查询一下之前旧的状态，是否删除
                        //删除的状态只能在执行删除或者还原的时候进行改变，此处仍然取旧的状态
                        var olddraft = draftdal.GetPri_ArticleDraft(ArticleKey);
                        //只有有旧的记录才更新
                        if (olddraft != null)
                        {
                            //必须在Draft中有这条记录修改才能生效，也就是说，只有点了保存按钮，产生了数据，才能够和temp数据进行关联，这样的话可以减少草稿内容（比如测试、或者打开页面没有做任何事情）的产生。
                            draftdal.UpdatePri_ArticleDraft(new Entity.Pri_ArticleDraft_Entity()
                            {
                                ArticleKey = ArticleKey,
                                ArticleTag = ArticleTag ?? "",
                                ArticleDesc= ArticleDesc??"",
                                AidStyle = AidStyle ?? "",
                                ArticleCategory = ArticleCategory ?? "",
                                ArticleTitle = ArticleTitle ?? "",
                                ContentText = ContentText ?? "",
                                DelStatus = olddraft == null ? (byte)0 : olddraft.DelStatus,
                                UpdateTime = DateTime.Now
                            });
                        }
                    }

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


        /// <summary>
        /// 对文章进行发布或者取消发布操作，注意：该方法只是对Pub_Article表进行了操作，能够执行发布的前提是存在draft数据并且不是draft数据没有删除
        /// </summary>
        /// <param name="ArticleKey"></param>
        /// <param name="IsPublish"></param>
        /// <returns></returns>
        public object Publish(string ArticleKey, bool IsPublish)
        {
            string _msg = string.Empty;
            int _status = 0;
            //先判断是否已经存在了Draft中去
            var draftentity = draftdal.GetPri_ArticleDraft(ArticleKey);
            //发布按钮展示的时候，已经进行过限制，此处为了保险，使用服务端限制
            if (draftentity == null||draftentity.DelStatus==1) //如果没有数据或者数据已经删除，需要重新保存
            {
                _status = 2;
                _msg = "请先保存数据";

            }
            else
            {
                OctOcean.DataService.Pub_Article_Dal padal = new DataService.Pub_Article_Dal();
                try
                {
                    padal.InsertPub_ArticleWithPri_ArticleDraft(ArticleKey, IsPublish);
                    _status = 1;
                }
                catch (Exception ex)
                {
                    _status = 4;
                    _msg = ex.Message;
                }

            }
            return new { status = _status, msg = _msg };

        }



        /// <summary>
        /// 删除文章，如果文章已经在draft进行过删除，就彻底删除该文章所有的记录，否则更新draft和发布后的删除状态
        /// </summary>
        /// <param name="ArticleKey"></param>
        /// <returns></returns>
        public object Delete(string ArticleKey)
        {
            int _status = 0;
            string _msg = "";
            try
            {
             //如果没有执行过删除，就更新一下状态，否则就彻底删除
                draftdal.DeleteAndClearTemp(ArticleKey);
                _status = 1;
            }
            catch (Exception ex)
            {
                _status = 4;
                _msg = ex.Message;

            }
            return new { status = _status, msg = _msg };


        }

        /// <summary>
        /// 分页功能
        /// </summary>
        /// <param name="orderColumn"></param>
        /// <param name="orderType"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="ArticleCategoryCode"></param>
        /// <param name="DelStatus"></param>
        /// <returns></returns>
        [Route("ArticleDraft/Pagination")] //Post和GET都可以访问
        public object Pager(string orderColumn = "UpdateTime", string orderType = "desc", int PageIndex = 1, int PageSize = 10, string ArticleCategoryCode = "", int DelStatus = 0,string ArticleTag="")
        {
            if ("UpdateTimeF".Equals(orderColumn,StringComparison.InvariantCultureIgnoreCase)) orderColumn = "UpdateTime";
            int sumcount = 0;
            //int PageIndex = page;
            //int PageSize = limit;
            IList<Aux_ArticleDraftPager_Entity> data = null;
            string where = "";
            if (DelStatus == 0)
            {
                where = " d.DelStatus=0 ";
            }
            else if (DelStatus == 1)
            {
                where = " d.DelStatus=1 ";
            }
            else
            {
                where = " 1=1 ";
            }

            if (!string.IsNullOrWhiteSpace(ArticleCategoryCode))
            {
                where += " AND d.ArticleCategory=@ArticleCategory ";
            }
           if(!string.IsNullOrWhiteSpace(ArticleTag))
            {
                where += " AND ArticleTag LIKE '%:'+@ArticleTag+':%' ";
            }
            var obj = new {
                ArticleCategory = ArticleCategoryCode,
                ArticleTag= ArticleTag
            };

            data = draftdal.GetPri_ArticleDraftPagerList(where, PageIndex, PageSize, obj, orderColumn, orderType, out sumcount);


            //if (string.IsNullOrEmpty(ArticleCategoryCode))
            //{

            //    data = draftdal.GetPri_ArticleDraftPagerList(where, PageIndex, PageSize, null, orderColumn, orderType, out sumcount);

            //}
            //else
            //{
                
            //    data = draftdal.GetPri_ArticleDraftPagerList(where, PageIndex, PageSize, new { ArticleCategory = ArticleCategoryCode }, orderColumn, orderType, out sumcount);
            //}
            return new { code = 0, msg = "", count = sumcount, data = data };

        }
    }
}