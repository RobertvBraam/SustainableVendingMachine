using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SustainableVendingMachine.Domain.Entities;
using SustainableVendingMachine.Domain.UseCases;
using SustainableVendingMachine.Host.Web.Hubs;

namespace SustainableVendingMachine.Host.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR();
            services.AddScoped<PurchaseProductUseCase>();

            var inventory = new List<ProductSlot>
            {
                new ProductSlot(Product.Tea, 10),
                new ProductSlot(Product.Espresso, 20),
                new ProductSlot(Product.Juice, 20),
                new ProductSlot(Product.ChickenSoup, 15)
            };
            var purse = new List<CoinSlot>
            {
                new CoinSlot(Coin.TenCents, 100),
                new CoinSlot(Coin.TwentyCents, 100),
                new CoinSlot(Coin.FiftyCents, 100),
                new CoinSlot(Coin.OneEuro, 100)
            };

            services.AddSingleton(new VendingMachine(inventory, purse));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<VendingMachineHub>("/VendingMachine");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
