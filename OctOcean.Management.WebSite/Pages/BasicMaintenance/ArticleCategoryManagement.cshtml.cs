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