using DevExpress.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContext;
using WebApplication1.Models;
using WebApplication1.Repository;
using DevExpress.AspNetCore.Reporting;
using Microsoft.Extensions.FileProviders;
using DevExpress.AspNetCore.Reporting;
using WebApplication1.Controllers;
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDevExpressControls();
            builder.Services.AddMvc();
            builder.Services.ConfigureReportingServices(configurator =>
            {
                configurator.ConfigureWebDocumentViewer(ViewerConfigurator =>
                {
                    ViewerConfigurator.UseCachedReportSourceBuilder();
                });
               
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DBContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
            );
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 9;
                }
                )
                .AddEntityFrameworkStores<DBContext>();
            var app = builder.Build();
            app.UseDevExpressControls();
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var env = builder.Environment;
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "StaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Customer}/{action=AddNewCustomer}/{id?}");

            app.Run();
        }
    }
}
