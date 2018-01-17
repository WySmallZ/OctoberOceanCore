using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OctOcean.Management.WebSite.Models;

namespace OctOcean.Management.WebSite.Pages
{
    public class IndexModel : PageModelBase
    {

        public string Message { get; private set; } = $"不积跬步,无以至千里;不积小流,无以成江海……";

        public IActionResult OnGet()
        {
            if (base.CheckLogin())
                return Page();
            else return Redirect("/login/index"); 
            //byte [] o= null;
            //if(HttpContext.Session!=null&&HttpContext.Session.TryGetValue("_octocean__user_", out o))
            //{
            //    if (System.Text.Encoding.Default.GetString(o) == "121222")
            //    {
            //        return Page();
            //    }
            //}

            //return Redirect("/login/index");

        }
    }
}