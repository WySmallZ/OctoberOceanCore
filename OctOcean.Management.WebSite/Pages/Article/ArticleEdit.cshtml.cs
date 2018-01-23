using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OctOcean.Entity;
using OctOcean.DataService;
namespace OctOcean.Management.WebSite.Pages.Article
{
    public class ArticleEditModel : Models.PageModelBase
    {
        [BindProperty]
        public Pri_ArticleDraft_Entity ArticleDraftEntity { get; set; }

        [BindProperty]
        public string ArticleGuidKey { get; set; }

        public bool IsPublish { get; set; }

        public bool CanUploadOrPublish { get; set; }

        public SelectList Base_ArticleCategoryddl { get; set; }

        public IList<Base_ArticleTag_Entity> Base_ArticleTagList { get; set; }

        public IList<Pri_ArticleImage_Entity> Pri_ArticleImageList { get; set; }


        //public string SelectArticleCategoryValue { get; set; }

        Pri_ArticleDraft_Dal dal = new Pri_ArticleDraft_Dal();

        public async Task<IActionResult> OnGetAsync(string ArticleKey)
        {
            if(!base.CheckLogin())
            {
                return Redirect("/login/index");
            }
            ViewData["ArticlePreviewUrl"] = OctOcean.Utils.OctOceanGlobal.Config.ArticlePreviewUrl+"/"+ArticleKey+"?t=p";

            //如果没有key，生成key
            if (string.IsNullOrEmpty(ArticleKey))
            {
                this.ArticleGuidKey = "A_"+Guid.NewGuid().ToString().Replace("-", "");
                IsPublish = false;
                CanUploadOrPublish = false;

            }
            else
            {
                this.ArticleGuidKey = ArticleKey;

                //获取信息
                this.ArticleDraftEntity = dal.GetPri_ArticleDraft(ArticleKey);


                if (ArticleDraftEntity == null)
                {
                    return RedirectToPage("ArticleManagement");
                }

                await Task.Run(() =>
                {

                  

                    this.ArticleDraftEntity.ArticleTag = this.ArticleDraftEntity.ArticleTag ?? "";

                    //判断是否已经发布
                    var pubarticle = new OctOcean.DataService.Pub_Article_Dal().GetPub_Article_Entity(ArticleKey);
                    IsPublish = pubarticle != null && pubarticle.DelStatus == 0; //如果存在数据，并且未删除就表示是发布过的

                    //能够进行上传操作和发布操作的前提是该文章必须进行过保存而且没有删除
                    CanUploadOrPublish = ArticleDraftEntity != null && ArticleDraftEntity.Id > 0 && ArticleDraftEntity.DelStatus == 0;

                });

            }
            BindControll(ArticleKey);
            return Page();

        }


        //public void OnGet(string ArticleKey)
        //{
        //    BindControll();
        //}


        public async Task<IActionResult> OnPostAsync()
        {

            this.ArticleDraftEntity.UpdateTime = DateTime.Now;
            this.ArticleDraftEntity.ArticleKey = this.ArticleGuidKey; 
            this.ArticleDraftEntity.ArticleTag = this.ArticleDraftEntity.ArticleTag ?? "";
            this.ArticleDraftEntity.ArticleCategory = this.ArticleDraftEntity.ArticleCategory ?? "";
            this.ArticleDraftEntity.ArticleDesc = this.ArticleDraftEntity.ArticleDesc ?? "";
            this.ArticleDraftEntity.AidStyle = this.ArticleDraftEntity.AidStyle ?? "";
            this.ArticleDraftEntity.DelStatus = 0;//通过保存提交之后，状态就更新为未删除，该功能可以对删除过的数据进行还原。
            this.ArticleDraftEntity.ContentText = new DataService.Pri_ArticleImage_Dal().ReplaceImagesPlaceholder(ArticleDraftEntity.ContentText, ArticleGuidKey);

            //if (!string.IsNullOrEmpty(this.ArticleDraftEntity.ArticleTag.Trim()))
            // {
            //     this.ArticleDraftEntity.ArticleTag = ':' + this.ArticleDraftEntity.ArticleTag.Replace(":", ":") + ':';
            // }

            await Task.Run(() =>
            {
                if (this.ArticleDraftEntity.Id > 0)
                {
                    dal.UpdatePri_ArticleDraft(ArticleDraftEntity);
                }
                else
                {

                    dal.InsertPri_ArticleDraft(this.ArticleDraftEntity);
                }



            });

            return RedirectToPage(new { ArticleKey = ArticleDraftEntity.ArticleKey });


            //BindControll(ArticleKey);

            //return Page();


        }

        private void BindControll(string ArticleKey)
        {
            //获取文章分类
            Base_ArticleCategory_Dal bacdal = new Base_ArticleCategory_Dal();
            this.Base_ArticleCategoryddl = new SelectList(bacdal.GetAllArticleCategory(), "ArticleCategoryCode", "ArticleCategoryName", ""); //默认选择空值

            Base_ArticleTag_Dal batdal = new Base_ArticleTag_Dal();
            Base_ArticleTagList = batdal.GetAllArticleTag();

            Pri_ArticleImage_Dal imgdal = new Pri_ArticleImage_Dal();
            Pri_ArticleImageList = imgdal.GetAllPri_ArticleImage(ArticleKey);
            
        }
    }
}