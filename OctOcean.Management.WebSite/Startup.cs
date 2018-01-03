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

        public IConfiguration Configuration { get; } //û��set��ֻ���ڹ��캯���н��и�ֵ

        

        //ConfigureServices ����Ӧ����ʹ�õķ����� ASP.NET Core MVC��Entity Framework Core �ͱ�ʶ��
        public void ConfigureServices(IServiceCollection services)
        {
            InitLoad();

            services.AddMvc();
           
            //.AddRazorPagesOptions(options=> { options.RootDirectory = "/wy"; });���������ļ�����Ϊ��Ŀ¼
            //.AddRazorPagesOptions(options =>
            // {
            //     options.RootDirectory = "/MyPages"; ����ҳ��ĸ�Ŀ¼
            //     options.Conventions.AuthorizeFolder("/MyPages/Admin");����Ϊҳ�����Ӧ�ó���ģ��Լ����
            // });


        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Configure ��������ܵ����м����

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseStaticFiles();//����ȥ������������css��js�޷����ص����
            //app.UseMvc();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }


        private void InitLoad()
        {
            //��ȡ�����ַ���
            ConfigHelper.DefaultConnectionString= Configuration.GetConnectionString("defaultConnStr");


        }
    }
}
