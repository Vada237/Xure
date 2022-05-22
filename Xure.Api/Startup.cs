using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xure.Data;
using Xure.Api.Interfaces;
using Xure.Api.Services;

namespace Xure.Api
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
            services.AddDbContext<AppDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("Data")));
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
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));           
            services.AddControllers();            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();             
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
