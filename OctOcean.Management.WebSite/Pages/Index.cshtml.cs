using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OctOcean.Management.WebSite.Pages
{
    public class IndexModel : PageModel
    {

        public string Message { get; private set; } = $"这是什么写法？{DateTime.Now}";

        public void OnGet()
        {
           
               
        }
    }
}