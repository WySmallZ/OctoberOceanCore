using System;
using System.Collections.Generic;
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
        [Route("{ArticleKey}/{UploadType?}")]
        public IActionResult Index(string ArticleKey, string UploadType)
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





        [Route("Send/{ArticleKey}")]
        public IActionResult SendSmallFile(string ArticleKey)
        {

            List<IFormFile> files = this.HttpContext.Request.Form.Files as List<IFormFile>;

            return Content("Post");

        }
    }
}