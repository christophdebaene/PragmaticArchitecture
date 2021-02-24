using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Application.Bootstrapper;
using MyApp.Domain;
using MyApp.Domain.Users;
using MyApp.Site.Infrastructure;
using Serilog;

namespace MyApp.Site
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMiniProfiler()
                .AddEntityFramework();

            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddMyApp(Configuration);
            services.AddScoped<IUserContext, SessionUserContext>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyAppContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseMiniProfiler();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
