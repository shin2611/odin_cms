using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ODIN_CMS.Models;
using System.Collections.Generic;
using Utils;
using Microsoft.AspNetCore.Http.Features;
using ODIN_CMS;

namespace ODIN_CMS
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
            services.AddControllersWithViews();
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 52428800;
            });
            services.AddAuthentication("LoginAuthen")
               .AddCookie("LoginAuthen", options =>
               {
                   options.AccessDeniedPath = new PathString("/Account/Access");
                   options.Cookie = new CookieBuilder
                   {
                       //Domain = "",
                       HttpOnly = true,
                       Name = "cookie",
                       Path = "/",
                       SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax,
                       SecurePolicy = CookieSecurePolicy.SameAsRequest
                   };
                   options.Events = new CookieAuthenticationEvents
                   {
                       OnSignedIn = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnSignedIn", context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       },
                       OnSigningOut = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnSigningOut", context.HttpContext.User.Identity.Name);
                           return Task.CompletedTask;
                       },
                       OnValidatePrincipal = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnValidatePrincipal", context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       }
                   };
                   options.ExpireTimeSpan = TimeSpan.FromHours(2);
                   options.LoginPath = new PathString("/Account/Login");
                   options.ReturnUrlParameter = "RequestPath";
                   options.SlidingExpiration = true;
               });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);//You can set Time   
                options.Cookie.IsEssential = true;
            });

            services.AddAuthorization();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            //.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.Configure<AppsettingModel>(Configuration.GetSection("AppSettings"));
            services.AddDistributedMemoryCache();
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory(MyAllowSpecificOrigins));
            //});
            services.AddHttpContextAccessor();
            services.AddSingleton<IdentityHelper>();
            Config.ConnectionString.SQLConnCMS = Configuration.GetConnectionString("SQLConnCMS");
            Config.URL_ROOT = Configuration.GetSection("URL_ROOT").Value;
            Config.MEDIA_DISK = Configuration.GetSection("MEDIA_DISK").Value;
            Config.IMAGE_URL = Configuration.GetSection("IMAGE_URL").Value;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            ODIN_CMS.AppContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=FormLogin}/{id?}");

                routes.MapRoute(
                    name: "/them-moi-lop-hoc",
                    template: "{controller=LevelOfClass}/{action=Index}");
            });
        }
    }
}
