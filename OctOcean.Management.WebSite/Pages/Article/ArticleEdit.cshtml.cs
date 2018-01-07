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
    public class ArticleEditModel : PageModel
    {
        [BindProperty]
        public Pri_ArticleDraft_Entity ArticleDraftEntity { get; set; }


        public SelectList Base_ArticleCategoryddl { get; set; }

        public IList<Base_ArticleTag_Entity> Base_ArticleTagList { get; set; }


        //public string SelectArticleCategoryValue { get; set; }

        Pri_ArticleDraft_Dal dal = new Pri_ArticleDraft_Dal();

        public async Task<IActionResult> OnGetAsync(string ArticleKey)
        {
            if (!string.IsNullOrEmpty(ArticleKey))
            {

                await Task.Run(() =>
                {
                    //获取信息
                    this.ArticleDraftEntity = dal.GetPri_ArticleDraft(ArticleKey);
                });

                if (ArticleDraftEntity == null)
                {
                    return RedirectToPage("ArticleManagement");
                }
            }
            BindControll();
            return Page();

        }


        //public void OnGet(string ArticleKey)
        //{
        //    BindControll();
        //}


        public async Task<IActionResult> OnPostAsync()
        {
            if( this.ArticleDraftEntity.ArticleTag == null)
            {
                this.ArticleDraftEntity.ArticleTag = string.Empty;
            }
            this.ArticleDraftEntity.UpdateTime = DateTime.Now;
           //if (!string.IsNullOrEmpty(this.ArticleDraftEntity.ArticleTag.Trim()))
           // {
           //     this.ArticleDraftEntity.ArticleTag = '[' + this.ArticleDraftEntity.ArticleTag.Replace(":", "][") + ']';
           // }
            
            await Task.Run(() => {
                if (string.IsNullOrEmpty(this.ArticleDraftEntity.ArticleKey))
                {
                    this.ArticleDraftEntity.ArticleKey = Guid.NewGuid().ToString().Replace("-", "");
                    dal.InsertPri_ArticleDraft(this.ArticleDraftEntity);
                }

                else
                {
                    dal.UpdatePri_ArticleDraft(ArticleDraftEntity);
                }
                
            });
            return RedirectToPage( new { ArticleKey = ArticleDraftEntity.ArticleKey });


            BindControll();
            
            return Page();


        }

        private void BindControll()
        {
            //获取文章分类
            Base_ArticleCategory_Dal bacdal = new Base_ArticleCategory_Dal();
            this.Base_ArticleCategoryddl = new SelectList(bacdal.GetAllArticleCategory(), "ArticleCategoryCode", "ArticleCategoryName", ""); //默认选择空值

            Base_ArticleTag_Dal batdal = new Base_ArticleTag_Dal();
            Base_ArticleTagList = batdal.GetAllArticleTag();


        }
    }
}