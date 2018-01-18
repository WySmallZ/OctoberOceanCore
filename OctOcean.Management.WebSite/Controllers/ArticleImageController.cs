using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OctOcean.DataService;
using OctOcean.Management.WebSite.Models;
using OctOcean.Utils;

namespace OctOcean.Management.WebSite.Controllers
{
    [Route("ArticleImage")] //所有的访问都得加上该前缀
    public class ArticleImageController : Controller
    {

        OctOcean.DataService.Pri_ArticleImage_Dal imgdal = new DataService.Pri_ArticleImage_Dal();
        [Route("Upload/{ArticleKey}")]
        public IActionResult Upload(string ArticleKey) //在文章编辑页面，点击上传时调用，可以同时上传多个图片文件
        {
            ViewBag.ArticleKey = ArticleKey;
            return View();
        }


        [Route("Send/{ArticleKey}")]
        public async Task<IActionResult> SendSmallFile(string ArticleKey) //同时上传多个文件时被调用
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
        public async Task<IActionResult> UpdateArticleImage(string ImgKey) //修改单个文件时被调用
        {
            var requestForm = HttpContext.Request.Form;
            int _status = -1;
            string _msg = "";
            string _fileName = "";
            if (requestForm != null)
            {
                List<IFormFile> files = requestForm.Files as List<IFormFile>;
                if (files != null && files.Count > 0)
                {
                    //获取旧的信息
                    var entity = imgdal.GetPri_ArticleImage_Entity(ImgKey);
                    if (entity == null)
                    {
                        _status = 4;
                        _msg = "ImgKey不存在";
                    }
                    else
                    {
                        string imageCacheDir = Path.Combine(OctOcean.Utils.OctOceanGlobal.Config.FileRoot_Cache_Image, entity.ArticleKey);
                        if (!Directory.Exists(imageCacheDir)) { Directory.CreateDirectory(imageCacheDir); };
                        var formFile = files[0];

                        string imgname = ImgKey + Path.GetExtension(formFile.FileName); //获取上传图片的扩展名
                        _fileName = imgname;

                        string fn = Path.Combine(imageCacheDir, imgname);
                        using (var stream = new FileStream(fn, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                            _status = 1;
                            _msg = Utils.OctOceanGlobal.Config.UrlRoot_Cache_Image + "/" + entity.ArticleKey + "/" + imgname;

                        }

                    }

                }
                else
                {
                    _status = 3;
                    _msg = "服务器没有获取到文件信息";

                }
            }
            else
            {
                _status = 2;
                _msg = "服务器未响应信息";
            }
            return Json(new { status = _status, msg = _msg, fileName = _fileName });

        }


        [Route("Confirm/{ArticleKey}")]
        public ActionResult ConfirmFile(string ArticleKey, string CacheFileNames) //确定图片时调用
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




        //编辑单个图片的时候
        [Route("Edit/{ImageKey}", Name = "image_edit")]
        public ActionResult Edit(string ImageKey)
        {
            OctOcean.Entity.Pri_ArticleImage_Entity entity = imgdal.GetPri_ArticleImage_Entity(ImageKey);
            if (entity == null) return Content("图片信息无效");
            return View(entity);
        }

        [HttpPost("Save")]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind("ImgKey,ImgName,Height,Width")]Entity.Pri_ArticleImage_Entity img_Entity, string NewImgSrc)
        {
            var entity = imgdal.GetPri_ArticleImage_Entity(img_Entity.ImgKey);
            if (entity == null) return Content("图片信息无效");
            //判断是否重新上传过新的文件
            if (!string.IsNullOrWhiteSpace(NewImgSrc))
            {
                string fullname = Path.Combine(Utils.OctOceanGlobal.Config.FileRoot_Cache_Image, entity.ArticleKey, img_Entity.ImgName);
                string newfullname = Path.Combine(Utils.OctOceanGlobal.Config.FileRoot_Web_Image, entity.ArticleKey, img_Entity.ImgName);
                //直接复制
                System.IO.File.Copy(fullname, newfullname, true);

                //修改旧的图片实体的信息
                entity.ImgName = img_Entity.ImgName;
                entity.Src = OctOceanGlobal.Config.UrlRoot_Web_Image + $"/{entity.ArticleKey}/{img_Entity.ImgName}";
            }




            entity.Height = img_Entity.Height;
            entity.Width = img_Entity.Width;
            entity.UpdateTime = DateTime.Now;
            imgdal.UpdatePri_ArticleImage(entity);



            // return Content("<script>alert('Hello World!');</script>", "application/x-javascript");
            //return RedirectToRoute("image_edit",new { ImageKey = entity.ImgKey });
            return RedirectToAction("Edit", new { ImageKey = entity.ImgKey });


        }

        [Route("Info/{ImgKey}")] //http://localhost:55730/articleimage/info/Img_4f756c2abfce4862a78e70e13dc0baad
        public Entity.Pri_ArticleImage_Entity GetImageInfo(string ImgKey)
        {
            return imgdal.GetPri_ArticleImage_Entity(ImgKey);

        }

        [Route("Remove/{ImgKey}")]
        public ActionResult RemoveImage(string ImgKey)
        {
            int _status = -1;
            string _msg = "";
            int i = 0;

            var entity = imgdal.GetPri_ArticleImage_Entity(ImgKey);
            if (entity == null) return Json(new { status = 2, msg = "数据获取失败" });
            try
            {
                i = imgdal.DeletePri_ArticleImageByImgKey(ImgKey);
                _status = 1;

            }
            catch (Exception ex)
            {
                _status = 4;
                _msg = ex.Message;

            }

            //删除数据
            if (i > 0)
            {
                //删除的时候有可能图片正在使用当中，不做处理
                try
                {

                    //删除图片和缓存
                    string f1 = Path.Combine(OctOceanGlobal.Config.FileRoot_Cache_Image, entity.ArticleKey, entity.ImgName);
                    if (System.IO.File.Exists(f1))
                    {
                        System.IO.File.Delete(f1);
                    }

                    string f2 = Path.Combine(OctOceanGlobal.Config.FileRoot_Web_Image, entity.ArticleKey, entity.ImgName);
                    if (System.IO.File.Exists(f2))
                    {
                        System.IO.File.Delete(f2);
                    }
                }
                catch (Exception)
                {
                }
            }

            return Json(new { status = _status, msg = _msg });
        }

        [HttpPost("Clear")]
        public ActionResult RunClearImage()
        {
            int _status = -1;
            string _msg = "";

            try
            {


                //获取系统所有图片
                var db_sys_all_iamges = new Pri_ArticleImage_Dal().GetAllPri_ArticleImage();
                string cp_web_root = OctOceanGlobal.Config.FileRoot_Web_Image;

                if (Directory.Exists(cp_web_root))
                {
                    //获取所有的articlekey所在目录
                    string[] cp_all_web_article_diries = Directory.GetDirectories(cp_web_root);

                    foreach (string cp_articlekey_dir in cp_all_web_article_diries)
                    {
                        string cp_articlekey = Path.GetFileNameWithoutExtension(cp_articlekey_dir + ".wy");//获取ArticleKey的值
                        var _db_sys_article_images_where = db_sys_all_iamges.Where(a => a.ArticleKey == cp_articlekey);                                                                //判断该目录是否正在使用当中
                        var db_sys_article_images = _db_sys_article_images_where == null ? null : _db_sys_article_images_where.Select(b => b.ImgName).ToArray();

                        //如果在数据库中没有找到当前目录正在使用当中，就删除改目录
                        if (db_sys_article_images == null || db_sys_article_images.Length == 0)
                        {
                            Directory.Delete(cp_articlekey_dir, true);
                        }
                        else
                        {
                            //获取当前key下的所有图片
                            string[] cp_all_web_article_images = Directory.GetFiles(cp_articlekey_dir);
                            foreach (string cp_img in cp_all_web_article_images)
                            {
                                //如果系统中没有使用该图片，就删除掉
                                if (!db_sys_article_images.Contains(Path.GetFileName(cp_img)))
                                {
                                    System.IO.File.Delete(cp_img);
                                }
                            }

                        }


                    }
                }

                //清理缓存
                if (Directory.Exists(OctOceanGlobal.Config.FileRoot_Cache_Image))
                {
                    Directory.Delete(OctOceanGlobal.Config.FileRoot_Cache_Image, true);

                }
                _status = 1;

            }
            catch (Exception ex)
            {

                _status = 4;
                _msg = ex.Message;
            }
            return Json(new { status = _status, msg = _msg });
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}