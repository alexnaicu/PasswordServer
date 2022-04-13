using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using PasswordServer.Api.Configurations;
using PasswordServer.Api.Contexts;
using PasswordServer.Api.Services;

namespace PasswordServer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
                
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddDbContext<PasswordServerContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString(PasswordConfiguration.ConnectionString)));

            services.AddScoped<IPasswordServerRepository, PasswordServerRepository>();
            services.Configure<PasswordConfiguration>(this.Configuration.GetSection(PasswordConfiguration.Name));
            services.AddTransient<IPasswordGenerator, DefaultPasswordGenerator>();
            services.AddTransient<IPasswordHasher, DefaultPasswordHasher>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())            
                app.UseDeveloperExceptionPage();            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");
            });
        }
    }
}