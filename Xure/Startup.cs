using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xure.Data;
using Xure.Api.Services;
using Xure.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Xure.App.Models;

namespace Xure.App
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
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Data")));

            services.AddIdentity<AppUser, IdentityRole>(options => {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "éöóêåíãøùçõúôûâàïğîëäæıÿ÷ñìèòüáşÉÖÓÊÅÍÃØÙÇÕÚÔÛÂÀÏĞÎËÄÆİß×ÑÌÈÒÜÁŞabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";                        
            });
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IProductSpecificationsRepository, ProductSpecificationsRepository>();
            services.AddTransient<IProductSpecificationsValueRepository, ProductSpecificationsValueRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IPriceHistoryRepository, PriceHistoryRepository>();
            services.AddTransient<IPriceRepository, PricesRepository>();
            services.AddTransient<IReceptionPointRepository, ReceptionPointRepository>();
            services.AddTransient<IDeliveryRepository, DeliveryRepository>();
            services.AddTransient<ISellerOrderRepository, SellerOrderRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IOrderProductRepository, OrderProductRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IReasonRepository, ReasonRepository>();
            services.AddTransient<IReviewsRepository, ReviewsRepository>();
            services.AddTransient<IOrderReportRepository, OrderReportRepository>();
            services.AddTransient<IProductReportRepository, ProductReportRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<Product>();
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });     
        }
    }
}
