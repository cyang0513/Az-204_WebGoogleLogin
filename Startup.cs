using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGoogleLogin.Data;

namespace WebGoogleLogin
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      // Auth 2.0
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection")));
         services.AddDatabaseDeveloperPageExceptionFilter();

         services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
             .AddEntityFrameworkStores<ApplicationDbContext>();
         services.AddControllersWithViews();

         //Add google login
         services.AddAuthentication().AddGoogle(x =>
                                                {
                                                   x.ClientId = "821329774779-nfnbogikvg98j8o7ppjspm8ijnu4vvdm.apps.googleusercontent.com";
                                                   x.ClientSecret = "3MjmNyKrXRDVoro5Za0f4bNU";
                                                }
            );
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {

         app.UseDeveloperExceptionPage();
         app.UseMigrationsEndPoint();

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
            endpoints.MapRazorPages();
         });
      }
   }
}
