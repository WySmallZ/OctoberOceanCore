using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OctOcean.Management.WebSite.Pages.Article
{
    public class ArticleFormatCodeModel : PageModel
    {
      
        [BindProperty]
        public string ContentText { get; set; }
        [BindProperty]
        public string OldString { get; set; } = "<";
        [BindProperty]
        public string NewString { get; set; } = "&lt;";
        public void OnGet()
        {
            ViewData["CurrentPage"] = "AFC";
            var s = ContentText;
        }

        public void OnPost()
        {
            string ct = this.ContentText;
            this.ContentText= ct.Replace(OldString, NewString);
            this.ViewData["ContentText"] = ContentText;



        }
    }
}