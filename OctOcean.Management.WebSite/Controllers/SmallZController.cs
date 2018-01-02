using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OctOcean.Management.WebSite.Controllers
{
    public class SmallZController : Controller
    {
        public IActionResult Index()
        {
            this.ViewBag.abc = "sd";
            return View();
        }





        /*
         允许使用以下方式进行调用：
              http://localhost:55730/smallz/welcome
              http://localhost:55730/smallz/welcome?name=2y
              http://localhost:55730/smallz/welcome?name=2y&numtimes=5
              只要控制器和方法名正确，都会触发该方法的执行，不同之处在于传入的参数
         */
        public string welcome(string name,int numtimes = 1)
        {
            //使用 HtmlEncoder.Default.Encode 防止恶意输入（即 JavaScript）损害应用
            return HtmlEncoder.Default.Encode($"hello {name},numtimes is {numtimes}"); 
        }
    }
}