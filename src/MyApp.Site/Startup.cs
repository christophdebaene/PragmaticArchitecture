using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Application.Bootstrapper;
using MyApp.Domain.Model;
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
            services.AddRazorPages();               
            services.AddHttpContextAccessor();
            services.AddMyApp(Configuration);
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

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
