using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OctOcean.Management.WebSite.Models;

namespace OctOcean.Management.WebSite.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Active(LoginUser_M  loginUser)
        {

            if(loginUser.UserName=="OctOcean"&&loginUser.PassWord=="smallzgogo")
            {
               
                HttpContext.Session.Set("_octocean__user_", System.Text.Encoding.Default.GetBytes("121222"));
                return RedirectToPage("/Index");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_octocean__user_");
            
            return Redirect("/Login/Index");
        }
    }
}