using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPractice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ApiPractice.Interfaces;
using ApiPractice.Classes;
using Microsoft.AspNetCore.Http;

namespace ApiPractice
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


            //

            services.AddTransient<IOperationTransient, DependencyInjectionDemo>();
            services.AddScoped<IOperationScoped, DependencyInjectionDemo>();
            services.AddSingleton<IOperationSingleton, DependencyInjectionDemo>();
            services.AddSingleton<IOperationSingletonInstance>(new DependencyInjectionDemo(Guid.Empty));
            //services.AddTransient<OperationService, DependencyInjectionDemo>();

            services.AddDbContext<UserContext>(op => op.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Before Invoke from 1st\n");
            //    await next();
            //    await context.Response.WriteAsync("After Invoke from 1st-2\n");
            //});


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello from 1st run\n");
            //});

            //// the following will never be executed    
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello from 2nd run\n");
            //});

            //app.Map("/api/users", appMap => {
            //    appMap.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Hello from 2nd app.Map()");
            //    });
            //});
            //app.UseWhen(async (context, next) =>
            //{

            //});


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<MiddlewareTest>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            

        }
    }
}
