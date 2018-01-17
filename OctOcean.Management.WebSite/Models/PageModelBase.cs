using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OctOcean.Management.WebSite.Models
{
    public class PageModelBase :PageModel
    {
        public bool CheckLogin()
        {
            byte[] o = null;
            if (HttpContext.Session != null && HttpContext.Session.TryGetValue("_octocean__user_", out o))
            {
                if (o!=null&&System.Text.Encoding.Default.GetString(o) == "121222")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
