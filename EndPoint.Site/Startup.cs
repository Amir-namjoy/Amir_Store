using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Carts;
using Amir_Store.Application.Services.Common.Queries.GetCategory;
using Amir_Store.Application.Services.Common.Queries.GetHomePageImages;
using Amir_Store.Application.Services.Common.Queries.GetMenuItem;
using Amir_Store.Application.Services.Common.Queries.GetSliders;
using Amir_Store.Application.Services.Finances.Commands.AddRequestPay;
using Amir_Store.Application.Services.Finances.Queries.GetRequestPayService;
using Amir_Store.Application.Services.HomePage.AddHomePageImages;
using Amir_Store.Application.Services.HomePage.AddNewSlider;
using Amir_Store.Application.Services.Orders.Commands.AddNewOrder;
using Amir_Store.Application.Services.Orders.Queries.GetUserOrders;
using Amir_Store.Application.Services.Products.FacadePatterns;
using Amir_Store.Application.Services.Users.Commands.RegisterUser;
using Amir_Store.Application.Services.Users.Commands.UserLogin;
using Amir_Store.Application.Services.Users.FacadePatterns;
using Amir_Store.Common.Roles;
using Amir_Store.Persistence.Contexts;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EndPoint.Site
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
                options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
                options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Authentication/Signin");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });

            services.AddScoped<IDataBaseContext, DataBaseContext>();

            //services.AddScoped<IGetUsersService, GetUsersService>();
            //services.AddScoped<IGetRolesService, GetRolesService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IUserLoginService, UserLoginService>();
            //services.AddScoped<IRemoveUserService, RemoveUserService>();
            //services.AddScoped<IUserStatusChangeService, UserStatusChangeService>();
            //services.AddScoped<IEditUserService, EditUserServices>();

            //Facade Injection
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddScoped<IProductFacade, ProductFacade>();

            //---------------------------------------------------------
            services.AddScoped<IGetMenuItemService, GetMenuItemService>();
            services.AddScoped<IGetCategoryService, GetCategoryService>();
            services.AddScoped<IAddNewSliderService, AddNewSliderService>();
            services.AddScoped<IGetSliderService, GetSliderService>();
            services.AddScoped<IAddHomePageImagesService, AddHomePageImagesService>();
            services.AddScoped<IGetHomePageImagesService, GetHomePageImagesService>();
            services.AddScoped<ICartServices, CartService>();
            services.AddScoped<IAddRequestPayService, AddRequestPayService>();
            services.AddScoped<IGetRequestPayService, GetRequestPayService>();
            services.AddScoped<IAddNewOrderService, AddNewOrderService>();
            services.AddScoped<IGetUserOrdersService, GetUserOrdersService>();
            
            services.AddScoped<CookiesManager>();

            string ConnectionString = @"Data Source=.; Initial Catalog=Amir_StoreDB; Integrated Security=True;";
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(ConnectionString));
            //services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(Configuration.GetConnectionString());
            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
            });
        }
    }
}
