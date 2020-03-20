using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sereno.Domain.EntityFramework;

namespace Sereno.STS
{
    public static class Startup
    {
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration, Action<DbContextOptionsBuilder> dbOptions)
        {
            services.AddDbContext<DataContext>(dbOptions);
            services.AddAspNetIdentity();
            services.AddIdentityServer4();

            services.AddAuthentication();
            services.AddAuthorization();

            return services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static IApplicationBuilder AddApi(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            

            app.UseRouting();

            app.UseAuthorization();
            app.UseIdentityServer();
           

            return app;
        }
    }
}