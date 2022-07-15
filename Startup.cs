using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform.Services;
using Microsoft.EntityFrameworkCore;
using Platform.Models;

namespace Platform
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedSqlServerCache(opts =>
            {
                opts.ConnectionString = Configuration["ConnectionStrings:CacheConnection"];
                opts.SchemaName = "dbo";
                opts.TableName = "DataCache";
            });
            services.AddResponseCaching();
            services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
            services.AddDbContext<CalculationContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:CalcConnection"]);
            });
            services.AddTransient<SeedData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime lifetime,SeedData data)
        {
            app.UseDeveloperExceptionPage();
            app.UseResponseCaching();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapEndpoint<SumEndpoint>("/sum/{count:int=1000000000}");
                endpoints.MapGet("/",async (context) =>
                    await context.Response.WriteAsync("Hello World!"));
            });

            bool cmdLineInit = (Configuration["INITDB"] ?? "false") == "true";
            if(env.IsDevelopment() || cmdLineInit)
            {
                lifetime.StopApplication();
            }
        }
    }
}