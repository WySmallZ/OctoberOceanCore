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
    public class ArticleCategoryEditModel : PageModel
    {
        Base_ArticleCategoryDal dal = new Base_ArticleCategoryDal();


        [BindProperty] //使用 [BindProperty] 特性来选择加入模型绑定
        public Base_ArticleCategory ArticleCategoryEntity { get; set; }





        public async Task<IActionResult> OnGetAsync(int id)
        {

            if (id > 0)
            {
                await Task.Run(() =>
                {
                    //获取信息
                    this.ArticleCategoryEntity = dal.GetArticleCategory(id);


                });
                if (ArticleCategoryEntity == null)
                {
                    return RedirectToPage("ArticleCategoryManagement");
                }

            }

            return Page();
        }

        //[TempData]
        //public string Message { get; set; }


        //当点击了submit时，执行了post动作调用OnPost**方法
        public async Task<IActionResult> OnPostAsync() //页面包含 OnPostAsync 处理程序方法，它在 POST 请求上运行（当用户发布窗体时）
        {
            //if (!ModelState.IsValid) //验证模型状态是否无效
            //{
            //    //提交的窗体存在（已传递到服务器的）验证错误时，OnPostAsyncData 处理程序方法调用 Page 帮助程序方法
            //    return Page(); //如果有错误，则再次显示页面并附带验证消息
            //}
             
            //获取ArticleCategoryEntity的值
            //写入ArticleCategoryEntity
            //await _db.SaveChangesAsync();
            await Task.Run(()=> {
                if (this.ArticleCategoryEntity.Id > 0)
                {
                    dal.UpdateArticleCategory(ArticleCategoryEntity);
                }
                else
                {
                    dal.InsertArticleCategory(this.ArticleCategoryEntity);
                }
            }); //此处模拟写入

            //Message = $"Customer wysmallz added"; //可以通过TempData传递参数
            ////跳转到列表界面
            return RedirectToPage("ArticleCategoryManagement"); //返回当前目录下的
        }


        //public void OnGet()
        //{
        //    //获取信息
        //    // this.ArticleCategoryEntity = new ArticleCategory() { ArticleCategoryCode = "A01", ArticleCategoryName = "Java", Id = 1 };
        //}


    }
}