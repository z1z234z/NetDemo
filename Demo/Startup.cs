using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading;
using UEditor.Core;
using Microsoft.Extensions.FileProviders;
using System.IO;
using WebSoketDLL;
using Utils;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace Demo
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
            services.AddDbContext<DBContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));
            services.AddUEditorService();
            try
            {
                services.AddMvc().ConfigureApplicationPartManager(manager =>
                {
                    //移除ASP.NET CORE MVC管理器中默认内置的MetadataReferenceFeatureProvider，该Provider如果不移除，还是会引发InvalidOperationException: Cannot find compilation library location for package 'MyNetCoreLib'这个错误
                    manager.FeatureProviders.Remove(manager.FeatureProviders.First(f => f is MetadataReferenceFeatureProvider));
                    //注册我们定义的ReferencesMetadataReferenceFeatureProvider到ASP.NET CORE MVC管理器来代替上面移除的MetadataReferenceFeatureProvider
                    manager.FeatureProviders.Add(new ReferencesMetadataReferenceFeatureProvider());
                });
            }
            catch (Exception e) {
                services.AddMvc();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseWebSockets();
            app.UseMiddleware<ChatWebSocketMiddleware>();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "upload")),
                RequestPath = "/upload",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=36000");
                }
            });
        }

       
    }
}
