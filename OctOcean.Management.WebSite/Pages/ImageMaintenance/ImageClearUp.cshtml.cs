using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OctOcean.DataService;
using OctOcean.Utils;
using System.IO;
using OctOcean.Management.WebSite.Models;

namespace OctOcean.Management.WebSite.Pages.ImageMaintenance
{
    public class ImageClearUpModel : PageModelBase
    {


       public Dictionary<string, string[]> dicClearImages = new Dictionary<string, string[]>();
        public string UrlRoot_Cache_Image { get; private set; }= OctOceanGlobal.Config.UrlRoot_Cache_Image;

        public bool BtnDisabled { get; set; }

        public IActionResult OnGet()
        {
            ViewData["CurrentPage"] = "ICU";

            if (!base.CheckLogin())
            {
                return Redirect("/login/index");
            }


            //获取系统所有图片
            var sys_all_iamges = new Pri_ArticleImage_Dal().GetAllPri_ArticleImage();

         
 
            string web_root = OctOceanGlobal.Config.FileRoot_Web_Image;
            if (Directory.Exists(web_root))
            {
                //获取所有的文章目录
                string[] all_web_article_diries = Directory.GetDirectories(web_root);
                foreach (string articlekey_dir in all_web_article_diries)
                {
                    string articlekey = Path.GetFileNameWithoutExtension(articlekey_dir+".wy");//获取ArticleKey的值



                    //获取当前key下的所有图片
                    string[] all_web_article_images = Directory.GetFiles(articlekey_dir);
                    //如果图片中存在一张没在使用的情况
                    dicClearImages[articlekey]= all_web_article_images.Select(c => Path.GetFileName(c))
                        .Where(a => sys_all_iamges.FirstOrDefault(b => b.ArticleKey == articlekey && b.ImgName == a) == null).ToArray();
 
                }
            }

            BtnDisabled= dicClearImages.Values.Sum(a => a.Length) == 0;

            return Page();

        }
         
    }
}