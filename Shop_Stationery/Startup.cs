
using dal;
using Entity;
using Entity.UserApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop_Stationery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;

namespace Shop_Stationery
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Hosting.IHostingEnvironment env) 
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange:true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
                

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //شناختن دیتا بیس به پروژه
            services.AddDbContext<db>(options => options.UseSqlServer(Configuration.GetConnectionString("con")));
            services.AddControllersWithViews();
            //services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("con"),
            //    optionsBuilder =>
            //    optionsBuilder.MigrationsAssembly("dal")
            //));

            services.AddIdentity<UserApp , IdentityRole>(option => 
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 6;
                option.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddUserManager<UserManager<UserApp>>()
            //.AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<db>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/PhoneNumber";//آدرس صفحه لاگین
                options.AccessDeniedPath = "/PhoneNumber";//آدرسی برای اینکه زمانی که کاربر به یک صفحه اجازه ذسترسی نداره این بیاد
                options.Cookie.Name = "stockssoheil.com";//اسم کوکی
                options.Cookie.HttpOnly = true;//برای اینکه ابزار های دیگه به جز این پروژه به کووکی دسترسی نداشته باشن
                options.ExpireTimeSpan = TimeSpan.FromDays(30);//مدت زمان لاگین بودن 
                options.SlidingExpiration = false;//این برای اینه که زمان لاگین بعد از خروج از سیات محاسبه بشه ولی اگه فالز باشه حتی اگه داخل سایت هم باشی از وقت کم میشه
            }
            );
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//مدت زمان وجود داشت سشن
            }
            );
    //        services
    //.AddDataProtection()
    //.SetApplicationName("stockssoheil.com")
    //.PersistKeysToFileSystem(new DirectoryInfo(@"\Keys\Keys.txt"));

            //services
            //    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            //        options =>
            //        {
            //            options.Cookie.Path = "/";
            //            options.LoginPath = "/PhoneNumber";//آدرس صفحه لاگین
            //            options.AccessDeniedPath = "/PhoneNumber";//آدرسی برای اینکه زمانی که کاربر به یک صفحه اجازه ذسترسی نداره این بیاد
            //            options.Cookie.Name = "stockssoheil.com";//اسم کوکی
            //            options.Cookie.HttpOnly = true;//برای اینکه ابزار های دیگه به جز این پروژه به کووکی دسترسی نداشته باشن
            //            options.ExpireTimeSpan = TimeSpan.FromDays(30);
            //        });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddHttpClient();
            services.AddAuthorization(option=> 
            {

                option.AddPolicy("AdminOnly",
                    policy => policy.RequireClaim("AdminNumber")
                    );

            });

            //services.AddRazorPages(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Keys" , "AdminOnly");
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //}
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    OnPrepareResponse = ctx =>
            //    {
            //        if (!ctx.Context.User.Identity.IsAuthenticated)
            //        {
            //            ctx.Context.Response.Redirect("/");
            //        }
            //    }
            //});

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    if (ctx.Context.Request.Path.StartsWithSegments("/Keys"))
                    {
                        //ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
                        if (!ctx.Context.User.Identity.IsAuthenticated)
                        {
                            ctx.Context.Response.Redirect("/phonenumber");
                        }
                    }
                }
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
