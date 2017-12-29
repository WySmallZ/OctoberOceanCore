using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OctOcean.WebAPI.DAL;
using OctOcean.WebAPI.Models;

namespace OctOcean.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")] //定义路由模板，此处的“[controller]”代表着当前的控制器名称，可以直接使用“ArticleDraft”代替
    public class ArticleDraftController : Controller
    {
        /*
         [HttpGet] 特性指定 HTTP GET 方法
             
             */

        ArticleDraft_MDal dal = new ArticleDraft_MDal();

        [HttpGet]
        public IList<ArticleDraft_M> GetAllArticleDraft()
        {

            return dal.GetAllArticleDraft();

            //web api会自动将集合的结果序列化为json
        }



        [HttpGet("{pageindex}/{pagesize}", Name = "GetAD")] //pagesize是占位符，它会将 URL 中“{pagesize}”的值分配给方法的 pagesize 参数。
        //Name = "GetAD" 创建具名路由
        public IActionResult GetArticleDraftsByPage(int pageindex,int pagesize)
        {


            var item = dal.GetAllArticleDraft().Skip((pageindex - 1) * pagesize).Take(pagesize);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
            //也可以参考GetAllArticleDraft方法的形式直接返回结果集
        }


        [HttpPost]
        public IActionResult Create([FromBody] ArticleDraft_M item) // [FromBody] 特性告诉 MVC 从 HTTP 请求正文获取项的值。
        {
            if (item == null)
            {
                return BadRequest();
            }
            //使用名为“GetAD”  的route来创建 URL。
            return CreatedAtRoute("GetAD", new { pagesize = 100 }, item); //将新添加的item通过GetAD具名路由输出显示
        }

    }
}