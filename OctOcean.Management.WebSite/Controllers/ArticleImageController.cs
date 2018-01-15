using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OctOcean.Management.WebSite.Models;
using OctOcean.Utils;

namespace OctOcean.Management.WebSite.Controllers
{
    [Route("ArticleImage")]
    public class ArticleImageController : Controller
    {

        OctOcean.DataService.Pri_ArticleImage_Dal imgdal = new DataService.Pri_ArticleImage_Dal();
        [Route("Upload/{ArticleKey}")]
        public IActionResult Upload(string ArticleKey)
        {
            //string _extensions = string.Empty;
            //string _mimeTypes = string.Empty;
            //string _title = string.Empty;
            ////switch (UploadType)
            ////{
            ////    case "img":
            ////        _extensions = "gif,jpg,jpeg,bmp,png";
            ////        _mimeTypes = "image/*";
            ////        _title = "Images";
            ////        break;
            ////    default:
            ////        break;
            ////}



            //Ex_UploadFile_M uploadmodel = new Ex_UploadFile_M()
            //{
            //    Accept_Extensions = _extensions,
            //    Accept_MimeTypes = _mimeTypes,
            //    Accept_Title = _title,
            //    Chunked = 0,
            //    ArticleKey = ArticleKey
            //};
            ViewBag.ArticleKey = ArticleKey;
            return View();
        }


        [Route("Send/{ArticleKey}")]
        public async Task<IActionResult> SendSmallFile(string ArticleKey)
        {
            var requestForm = HttpContext.Request.Form;
            string imageurl = Utils.OctOceanGlobal.Config.UrlRoot_Cache_Image + "/" + ArticleKey;
            string fileName = string.Empty;

            if (requestForm != null)
            {
                string imageCacheDir = Path.Combine(OctOcean.Utils.OctOceanGlobal.Config.FileRoot_Cache_Image, ArticleKey);

                if (!Directory.Exists(imageCacheDir)) { Directory.CreateDirectory(imageCacheDir); };




                string fileguid = requestForm["guid"]; //该Guid是上传控件自带的Guid，有wu前缀，程序里使用自动生成的Guid作为ImageKey
                string filename = requestForm["name"];
                //判断是否进行了分片

                List<IFormFile> files = this.HttpContext.Request.Form.Files as List<IFormFile>;
                if (files != null && files.Count > 0)
                {
                    var formFile = files[0];
                    string imgname = "Img_" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);
                    fileName = imgname;
                    string fn = Path.Combine(imageCacheDir, imgname);
                    using (var stream = new FileStream(fn, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);

                        imageurl = imageurl + "/" + imgname;
                    }
                    //只会有一个文件
                    //foreach (var formFile in files)
                    //{
                    //    string imgname = "Img_" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);
                    //    string fn = Path.Combine(imageCacheDir, imgname );
                    //    using (var stream = new FileStream(fn, FileMode.Create))
                    //    {
                    //        await formFile.CopyToAsync(stream);

                    //        imageurl= imageurl + "/" + imgname;
                    //    }

                    //}
                }



                return Json(new { status = 1, msg = imageurl, fileName = fileName });



                //

                //&& HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0
            }


            return Json(new { status = 0, msg = imageurl, fileName = fileName });

        }

        [Route("Update/{ImgKey}")]
        public async Task<IActionResult> UpdateArticleImage(string ImgKey)
        {
            return Json("");

        }


        [Route("Confirm/{ArticleKey}")]
        public ActionResult ConfirmFile( string ArticleKey, string CacheFileNames)
        {



            List<string> noErrorImg = new List<string>();
            int status = 1;
            string msg = "";
            string article_cachefile_root = Path.Combine(OctOcean.Utils.OctOceanGlobal.Config.FileRoot_Cache_Image, ArticleKey);

            //判断目标路径是否存在
            string article_sysfile_root = Path.Combine(OctOcean.Utils.OctOceanGlobal.Config.FileRoot_Web_Image, ArticleKey);
            if (!Directory.Exists(article_sysfile_root)) { Directory.CreateDirectory(article_sysfile_root); };


            string[] farr = CacheFileNames.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
          

            foreach (string fn in farr)
            {
                try
                {
                    string fullname = Path.Combine(article_cachefile_root, fn);
                    if (System.IO.File.Exists(fullname))
                    {
                        System.IO.File.Copy(fullname, Path.Combine(article_sysfile_root, fn), true);
                        //插入到图片表里面
                        imgdal.InsertPri_ArticleImage(new Entity.Pri_ArticleImage_Entity()
                        {
                            ArticleKey = ArticleKey,
                            Height = 0,
                            Width = 0,
                            ImgName = fn,
                            ImgKey = Path.GetFileNameWithoutExtension(fn),
                            Src = $"{OctOceanGlobal.Config.UrlRoot_Web_Image}/{ArticleKey}/{fn}",
                            UpdateTime = DateTime.Now
                        });

                        noErrorImg.Add(fn);
                    }
                }
                catch (Exception ex)
                {
                    status = 4;
                    msg += $"文件复制错误，文件名：{fn}，异常信息：{ex.Message}；\n";
                }

            }


            return Json(new { status = status, msg = msg });

        }

        [Route("Edit/{ImageKey}")]
        public ActionResult Edit(string ImageKey)
        {
            OctOcean.Entity.Pri_ArticleImage_Entity entity= imgdal.GetPri_ArticleImage_Entity(ImageKey);
            return View(entity);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}