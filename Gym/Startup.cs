using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using Gym.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates = true,
                CloseButton = true,
            });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
            services.AddDbContext<ApplicationDBContext>(options=>
            {
                options.UseSqlServer(configuration.GetConnectionString("SQLCON"));
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IApplicationRepository<Biography>, BiographyRepository>();
            services.AddScoped<IApplicationRepository<Slider>, SliderRepository>();
            services.AddScoped<IApplicationRepository<Doctor>, DoctorRepository>();
            services.AddScoped<IApplicationRepository<Blog>, BlogRepository>();
            services.AddScoped<IApplicationRepository<Group>, GroupRepository>();
            services.AddScoped<IApplicationRepository<Image>, ImagesRepository>();
            services.AddScoped<IApplicationRepository<Certificate>, CetificateRepository>();
            services.AddScoped<IApplicationRepository<Request>, RequestRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoits=> {
                endpoits.MapAreaControllerRoute(
                    name: "MyAdminArea",
                    areaName: "Admin",
                    pattern:"Admin/{controller=Dashboard}/{Action=Index}/{id?}"
                    );

                endpoits.MapControllerRoute(
                    name:"Default",
                    pattern:"{controller=Home}/{Action=Index}/{id?}");
            
            });


            
        }
    }
}
