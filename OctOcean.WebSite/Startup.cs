 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OctOcean.Utils;

namespace OctOcean.WebSite
{
    public class Startup
    {
        public IConfiguration Configuration { get; } //只读的属性只能在构造方法中进行赋值

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }



        //该方法被运行时调用。 使用此方法向容器添加服务
        public void ConfigureServices(IServiceCollection services)
        {
            //此处可以注册数据库上下文
            InitLoad();
     //       services.AddDbContext<DataService.OctOceanContext>(options =>
     //options.UseSqlServer(Configuration.GetConnectionString("defaultConnStr")));


            //将MVC服务添加到服务容器中
            services.AddMvc();
        }



        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment()) //检查当前的托管环境名称是否为“ 开发”。
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error"); //发生错误显示页
            //}

            app.UseStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller}/{action=Index}/{id?}");
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }

        private void InitLoad()
        {
            //获取连接字符串
           ConfigHelper.DefaultConnectionString = Configuration.GetConnectionString("defaultConnStr");


        }
    }
}
