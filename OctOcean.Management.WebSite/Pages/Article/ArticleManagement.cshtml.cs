using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OctOcean.DataService;
using OctOcean.Entity;

namespace OctOcean.Management.WebSite.Pages.Article
{
    public class ArticleManagementModel : PageModel
    {
        //Pri_ArticleDraft_Dal dal = new Pri_ArticleDraft_Dal();

        //public IList<Pri_ArticleDraft_Entity> ArticleDrafts { get; private set; }


        public SelectList Base_ArticleCategoryddl { get; set; }
        public SelectList Base_ArticleTagddl { get; set; }


        public async Task OnGetAsync()
        {
            //此处模拟获取数据
            await Task.Run(() =>
            {
                //获取文章分类
                Base_ArticleCategory_Dal bacdal = new Base_ArticleCategory_Dal();
                this.Base_ArticleCategoryddl = new SelectList(bacdal.GetAllArticleCategory(), "ArticleCategoryCode", "ArticleCategoryName", ""); //默认选择空值

                Base_ArticleTag_Dal tagdal = new Base_ArticleTag_Dal();
                var tagsource = tagdal.GetAllArticleTag();
                this.Base_ArticleTagddl = new SelectList(tagsource, "ArticleTagCode", "ArticleTagName", "");
                var obj = tagsource.ToDictionary(a => a.ArticleTagCode, b => b.ArticleTagName);
                ViewData["TagJson"]= JsonConvert.SerializeObject(obj);

               

            });


        }
    }
}