using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SiteMonitoring.Core.Extensions;
using SiteMonitoring.Core.Monitoring;
using SiteMonitoring.Core.Monitoring.Extensions;
using SiteMonitoring.Core.Monitoring.Interfaces;
using SiteMonitoring.Core.Monitoring.Services;
using SiteMonitoring.Core.Monitoring.Services.Interfaces;
using SiteMonitoring.Data;

namespace SiteMonitoring
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
            services.AddAuthOptions(Configuration, out var authOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidIssuer = authOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authOptions.SecurityKey
                    };
                });

            services.AddDbContext<DataContext>(o => o.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TEST;Trusted_Connection=True;"), ServiceLifetime.Transient);
            services.AddControllers();
            services.AddHttpClient<ISiteMonitorService, HttpSiteMonitoringService>();
            services.AddScoped<IMonitoringDataContext, MonitoringDataContext>();
            services.AddSiteMonitoringManager();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
