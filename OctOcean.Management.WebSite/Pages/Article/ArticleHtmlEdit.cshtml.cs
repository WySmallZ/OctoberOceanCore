using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OctOcean.Management.WebSite.Pages.Article
{
    public class ArticleHtmlEditModel : PageModel
    {
        [BindProperty]
        public string ContentText { get; set; }

        [BindProperty]
        public string ArticleKey { get; set; }

        public string IsSave { get; set; }
        OctOcean.DataService.Pri_ArticleDraft_Dal dal = new OctOcean.DataService.Pri_ArticleDraft_Dal();
        public void OnGet(string ArticleKey)
        {
            
            var entity =dal.GetPri_ArticleDraft(ArticleKey);
            this.ContentText = entity.ContentText;
            IsSave = "";
            
        }

        public void OnPost()
        {
            string ct = this.ContentText;

            dal.UpdatePri_ArticleDraftContentText(ArticleKey, ct);
            IsSave = "true";
            
        }
    }
}