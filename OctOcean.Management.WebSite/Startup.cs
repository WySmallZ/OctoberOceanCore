using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OctOcean.Utils;

namespace OctOcean.Management.WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } //没有set，只能在构造函数中进行赋值



        //ConfigureServices 定义应用所使用的服务（如 ASP.NET Core MVC、Entity Framework Core 和标识）
        public void ConfigureServices(IServiceCollection services)
        {
            InitLoad();

            services.AddMvc();
            services.AddSession(options=> {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                //options.Cookie.HttpOnly = true;
            });
            //services.AddSingleton(Configuration);

            //.AddRazorPagesOptions(options=> { options.RootDirectory = "/wy"; });配置其他文件夹作为根目录
            //.AddRazorPagesOptions(options =>
            // {
            //     options.RootDirectory = "/MyPages"; 设置页面的根目录
            //     options.Conventions.AuthorizeFolder("/MyPages/Admin");或者为页面添加应用程序模型约定。
            // });


        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Configure 定义请求管道的中间件。

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();//不可去掉，否则会出现css、js无法加载的情况
                                 //app.UseMvc();
            app.UseSession();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }


        private void InitLoad()
        {

            //获取连接字符串
            OctOceanGlobal.SetConfig(
               defaultConnectionString: Configuration.GetConnectionString("defaultConnStr")
               , fileRoot: Configuration.GetValue<string>("OctOcean:FileRoot")
               , urlRoot: Configuration.GetValue<string>("OctOcean:UrlRoot")
               , articlePreviewUrl: Configuration.GetValue<string>("OctOcean:ArticlePreviewUrl")
               
                );


        }
    }
}
