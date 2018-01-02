using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OctOcean.DataService;
using OctOcean.Entity;
namespace OctOcean.Management.WebSite.Pages.BasicMaintenance
{
    public class ArticleTagManagementModel : PageModel
    {
        Base_ArticleTag_Dal dal = new Base_ArticleTag_Dal();

        public IList<Base_ArticleTag_Entity> ArticleTags { get; private set; }

        //[TempData]
        //public string Message { get; set; } //用于参数传递



        //初次加载所有的数据，注意：Async 命名后缀为可选，但不可更改，但是按照惯例通常会将它用于异步函数
        public async Task OnGetAsync()
        {
            //此处模拟获取数据
            await Task.Run(() =>
            {
                this.ArticleTags = dal.GetAllArticleTag();
            });
        }


        //点击删除时进行的操作，此处的命名需要按照页面上设置的handler决定
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                dal.DeleteArticleTag(id);
            });

            return RedirectToPage();
        }


    }
}