using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.BLL.Common;
using MVC.BLL.Services.Abstraction;
using MVC.BLL.Services.Implementaion;
using MVC.DAL.Common;
using MVC.DAL.DataBase;
using MVC.DAL.Entities;
using MVC.DAL.Repo.Abstraction;
using MVC.DAL.Repo.Implementation;

namespace MVC.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("defultConnection");
            builder.Services.AddDbContext<MvcDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });



            builder.Services.AddIdentityCore<Employee>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<MvcDbContext>()  
                            .AddTokenProvider<DataProtectorTokenProvider<Employee>>(TokenOptions.DefaultProvider);


            builder.Services.AddIdentity<Employee, IdentityRole>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<MvcDbContext>();

            //builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            //builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();

            builder.Services.AddBusinessInDAl();
            builder.Services.AddBusinessInBLL();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
