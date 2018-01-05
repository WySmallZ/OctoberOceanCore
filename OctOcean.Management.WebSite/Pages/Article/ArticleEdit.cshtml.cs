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


        public void OnPost()
        {
            var a = this.ArticleDraftEntity;
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