using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OctOcean.Entity;

namespace OctOcean.Management.WebSite.Pages.BasicMaintenance
{
    public class ArticleCategoryEditModel : PageModel
    {
        [BindProperty] //使用 [BindProperty] 特性来选择加入模型绑定
        public ArticleCategory ArticleCategoryEntity { get; set; }


       
        public async Task<IActionResult> OnPostAsync() //页面包含 OnPostAsync 处理程序方法，它在 POST 请求上运行（当用户发布窗体时）
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var _test = this.ArticleCategoryEntity;
            //获取ArticleCategoryEntity的值
            //写入ArticleCategoryEntity
            //await _db.SaveChangesAsync();
            await Task.Run(()=> { }); //此处模拟写入
              return RedirectToPage("/BasicMaintenance/ArticleCategoryManagement");
        }


        //public void OnGet()
        //{
        //    //获取信息
        //   // this.ArticleCategoryEntity = new ArticleCategory() { ArticleCategoryCode = "A01", ArticleCategoryName = "Java", Id = 1 };
        //}


        public void OnGet(int Id)
        {
            //获取信息
            this.ArticleCategoryEntity = new ArticleCategory() { ArticleCategoryCode = "A01", ArticleCategoryName = "Java", Id = Id };
        }
    }
}