using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OctOcean.DataService;
using OctOcean.Entity;

namespace OctOcean.Management.WebSite.Pages.Article
{
    public class ArticleManagementModel : PageModel
    {
        Pri_ArticleDraft_Dal dal = new Pri_ArticleDraft_Dal();

        public IList<Pri_ArticleDraft_Entity> ArticleDrafts { get; private set; }

        public async Task OnGetAsync()
        {
            //此处模拟获取数据
            await Task.Run(() =>
            {
                this.ArticleDrafts = dal.GetAllPri_ArticleDraft();
                ViewData["ArticCount"] = ArticleDrafts.Count;
            });


        }
    }
}