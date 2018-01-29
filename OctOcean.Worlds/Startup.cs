using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OctOcean.Worlds
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
            //此处可以注册数据库上下文
            InitLoad();
            //       services.AddDbContext<DataService.OctOceanContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("defaultConnStr")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            //if (env.IsDevelopment())
            //{
            //    app.UseBrowserLink();
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{ArticleCategory?}");
            });
        }

        private void InitLoad()
        {

            //获取连接字符串
            Utils.OctOceanGlobal.SetConfig(
               defaultConnectionString: Configuration.GetConnectionString("defaultConnStr")
               , fileRoot: Configuration.GetValue<string>("OctOcean:FileRoot")
               , urlRoot: Configuration.GetValue<string>("OctOcean:UrlRoot")
                , articlePreviewUrl: Configuration.GetValue<string>("OctOcean:ArticlePreviewUrl")
                );


        }
    }
}
