using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InMemory_Project.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InMemory_Project
{
    public class Startup
    {
        string contentRoot = string.Empty;
        public Startup(IConfiguration configuration)
        {
            contentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectContext>(options => options.UseSqlite($"Data Source={contentRoot}\\app.sqlite"));
            //services.AddDbContext<ProjectContext>(options => options.UseSqlite(@"Data Source=D:\Work\GitHub\Caching\InMemory_Project\app.sqlite"));

            services.AddMemoryCache();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
