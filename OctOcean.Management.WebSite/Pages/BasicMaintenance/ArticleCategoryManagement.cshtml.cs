using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OctOcean.Entity;
namespace OctOcean.Management.WebSite.Pages.BasicMaintenance
{
    public class ArticleCategoryManagementModel : PageModel
    {
        
        public IList<ArticleCategory> ArticleCategorys { get; private set; }

        [TempData]
        public string Message { get; set; }



        //初次加载所有的数据，注意：Async 命名后缀为可选，但不可更改，但是按照惯例通常会将它用于异步函数
        public async Task OnGetAsync()
        {
            //此处模拟获取数据
            await Task.Run(() => {
                this.ArticleCategorys = new List<ArticleCategory>();
                this.ArticleCategorys.Add(new ArticleCategory()
                {
                    ArticleCategoryCode = "W01",
                    ArticleCategoryName = "Python",
                    Id = 1

                });

                this.ArticleCategorys.Add(new ArticleCategory()
                {
                    ArticleCategoryCode = "W02",
                    ArticleCategoryName = "Go",
                    Id = 2

                });

            }); 


        }

        //点击删除时进行的操作，此处的命名需要按照页面上设置的handler决定
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await Task.Run(()=> {
                var ac= this.ArticleCategorys.FirstOrDefault(a => a.Id == id);
                if (ac != null)
                {
                    this.ArticleCategorys.Remove(ac);
                }
            });
             
            return RedirectToPage();
        }


    }
}