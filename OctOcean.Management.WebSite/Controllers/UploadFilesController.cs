using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OctOcean.Management.WebSite.Models;

namespace OctOcean.Management.WebSite.Controllers
{
    [Route("Upload")]
    public class UploadFilesController : Controller
    {
        [Route("{UploadType}/{ArticleKey}")]
        public IActionResult Index( string UploadType, string ArticleKey)
        {
            string _extensions = string.Empty;
            string _mimeTypes = string.Empty;
            string _title = string.Empty;
            switch (UploadType)
            {
                case "img":
                    _extensions = "gif,jpg,jpeg,bmp,png";
                    _mimeTypes = "image/*";
                    _title = "Images";
                    break;
                default:
                    break;
            }



            Ex_UploadFile_M uploadmodel = new Ex_UploadFile_M()
            {
                Accept_Extensions = _extensions,
                Accept_MimeTypes = _mimeTypes,
                Accept_Title = _title,
                Chunked = 0,
                ArticleKey = ArticleKey
            };






            return View(uploadmodel);
        }





        [Route("Send/{FileType}/{ArticleKey}")]
        public async Task<IActionResult> SendSmallFile(string FileType,string ArticleKey)
        {
            var requestForm = HttpContext.Request.Form;
          
            if (requestForm!=null)
            {
                string imageCacheDir = Path.Combine(OctOcean.Utils.OctOceanGlobal.Config.FileRoot_Cache_Image, ArticleKey);
                if (!Directory.Exists(imageCacheDir)) { Directory.CreateDirectory(imageCacheDir); };




                string fileguid = requestForm["guid"]; //该Guid是上传控件自带的Guid，有wu前缀，程序里使用自动生成的Guid作为ImageKey
                string filename = requestForm["name"];
                //判断是否进行了分片
                if (requestForm.ContainsKey("chunk"))
                {
                    await Task.Run(()=> { });
                }
                else
                {
                    List<IFormFile> files = this.HttpContext.Request.Form.Files as List<IFormFile>;
                    if (files != null && files.Count > 0)
                    {
                        foreach (var formFile in files)
                        {
                            string imgname = "Img_" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);
                            string fn = Path.Combine(imageCacheDir, imgname );
                            using (var stream = new FileStream(fn, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                                
                            }

                        }
                    }
                }
               




                //

                //&& HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0
            }
            

            return  Content("Post");

        }
    }
}