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
    public class ArticleTagEditModel : PageModel
    {
        Base_ArticleTagDal dal = new Base_ArticleTagDal();


        [BindProperty] //使用 [BindProperty] 特性来选择加入模型绑定
        public Base_ArticleTag ArticleTagEntity { get; set; }





        public async Task<IActionResult> OnGetAsync(int id)
        {

            if (id > 0)
            {
                await Task.Run(() =>
                {
                    //获取信息
                    this.ArticleTagEntity = dal.GetArticleTag(id);


                });
                if (ArticleTagEntity == null)
                {
                    return RedirectToPage("ArticleTagManagement");
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

            //获取ArticleTagEntity的值
            //写入ArticleTagEntity
            //await _db.SaveChangesAsync();
            await Task.Run(() => {
                if (this.ArticleTagEntity.Id > 0)
                {
                    dal.UpdateArticleTag(ArticleTagEntity);
                }
                else
                {
                    dal.InsertArticleTag(this.ArticleTagEntity);
                }
            }); //此处模拟写入

            //Message = $"Customer wysmallz added"; //可以通过TempData传递参数
            ////跳转到列表界面
            return RedirectToPage("ArticleTagManagement"); //返回当前目录下的
        }


        //public void OnGet()
        //{
        //    //获取信息
        //    // this.ArticleTagEntity = new ArticleTag() { ArticleTagCode = "A01", ArticleTagName = "Java", Id = 1 };
        //}


    }
}