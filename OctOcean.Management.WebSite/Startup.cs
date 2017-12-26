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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitLoad();

            services.AddMvc();
           
            //.AddRazorPagesOptions(options=> { options.RootDirectory = "/wy"; });配置其他文件夹作为根目录
            //.AddRazorPagesOptions(options =>
            // {
            //     options.RootDirectory = "/MyPages"; 设置页面的根目录
            //     options.Conventions.AuthorizeFolder("/MyPages/Admin");或者为页面添加应用程序模型约定。
            // });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseStaticFiles();//不可去掉，否则会出现css、js无法加载的情况
            app.UseMvc();



            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller}/{action=Index}/{id?}");
            //});
        }


        private void InitLoad()
        {
            //获取连接字符串
            ConfigHelper.DefaultConnectionString= Configuration.GetConnectionString("defaultConnStr");


        }
    }
}
