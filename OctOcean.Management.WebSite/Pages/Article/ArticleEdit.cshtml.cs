using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OctOcean.Entity;

namespace OctOcean.Management.WebSite.Pages.Article
{
    public class ArticleEditModel : PageModel
    {
        [BindProperty]
        public ArticleDraft ArticleDraftEntity { get; set; }


        public void OnGet(string ArticleKey)
        {
            
           
        }
    }
}