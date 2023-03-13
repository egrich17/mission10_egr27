using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });

            // each http request gets its own repository object (decoupling)
            services.AddScoped<IBookRepository, EFBookRepository>();

            // use purchase repository
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();

            services.AddRazorPages();

            // add caching services
            services.AddDistributedMemoryCache();

            services.AddSession();

            // when we're talking about a basket, we are calling the get basket method
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));

            // provide access to current Http context if there is one
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            // set up different endpoints depending on user navigation
            app.UseEndpoints(endpoints =>
            {
                // if we have book type and page num
                endpoints.MapControllerRoute("typepage",
                    "{bookType}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                // if we have pageNum only
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1});

                // if we have type only
                endpoints.MapControllerRoute("type",
                    "{bookType}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                // default
                endpoints.MapDefaultControllerRoute();

                // enable razor pages
                endpoints.MapRazorPages();
            });
        }
    }
}
