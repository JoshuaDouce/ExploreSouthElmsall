using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreSouthElmsall.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreSouthElmsall
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(x => 
            new FeatureToggles {
                EnableDeveloperExceptions = _configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions") });

            services.AddDbContext<BlogDataContext>(options => {
                var conString = _configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(conString);
            });

            services.AddDbContext<IdentityDataContext>(options => {
                var conString = _configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(conString);
            });

            services.AddSingleton<FormattingService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FeatureToggles features)
        {
            

            if (features.EnableDeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/error.html");
            }

            app.UseIdentity();

            //Default route = home/index/id but id is optional
            app.UseMvc(routes => {
                routes.MapRoute(
                    "Default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseFileServer();
        }
    }
}
