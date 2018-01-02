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


        public SelectList Base_ArticleCategoryList { get; set; }
        public string SelectArticleCategoryValue { get; set; }

        public void OnGet(string ArticleKey,string sacv)
        {
            Base_ArticleCategory_Dal bac = new Base_ArticleCategory_Dal();

            this.Base_ArticleCategoryList = new SelectList(bac.GetAllArticleCategory(), "ArticleCategoryCode", "ArticleCategoryName","");
        }
    }
}