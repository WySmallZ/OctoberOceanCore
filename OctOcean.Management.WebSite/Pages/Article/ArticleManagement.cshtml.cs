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
        ArticleDraftDal dal = new ArticleDraftDal();

        public IList<ArticleDraft> ArticleDrafts { get; private set; }

        public async Task OnGetAsync()
        {
            //此处模拟获取数据
            await Task.Run(() =>
            {
                this.ArticleDrafts = dal.GetAllArticleDraft();
                ViewData["ArticCount"] = ArticleDrafts.Count;
            });


        }
    }
}