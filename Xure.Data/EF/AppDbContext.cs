using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Xure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories;
        public DbSet<Company> Companies;
        public DbSet<Message> Messages;
        public DbSet<Order> Orders;
        public DbSet<OrderProduct> OrderProducts;
        public DbSet<OrderReport> OrderReports;
        public DbSet<Product> Products;
        public DbSet<ProductReport> ProductReports;
        public DbSet<Reason> Reasons;
        public DbSet<ReceptionPoint> ReceptionPoints;
        public DbSet<Reviews> Reviews;
        
        
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>(
            c => {
                c.HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId);
                c.HasOne(c => c.Seller)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.SellerId);
            });
            builder.Entity<Company>(
               c =>
                  {
                      c.HasMany(c => c.Sellers)
                        .WithOne(c => c.Company)
                        .HasForeignKey(c => c.CompanyId);
                  });
            builder.Entity<Message>(
                c =>
                {
                    c.HasOne(c => c.Client)
                    .WithMany(c => c.ClientMessages)
                    .HasForeignKey(c => c.ClientId)
                    .OnDelete(DeleteBehavior.NoAction);
                    c.HasOne(c => c.Seller)
                    .WithMany(c => c.SellerMessages)
                    .HasForeignKey(c => c.SellerId)
                    .OnDelete(DeleteBehavior.NoAction);
                });
            builder.Entity<Order>(
                c =>
                {
                    c.HasOne(c => c.ReceptionPoint)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(c => c.ReceptionPointId);
                    c.HasOne(c => c.Client)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(c => c.ClientId);

                    c.HasMany(c => c.Products)
                    .WithMany(c => c.Orders)
                    .UsingEntity<OrderProduct>(
                        c => c.HasOne(c => c.Product)
                        .WithMany(c => c.OrderProducts)
                        .HasForeignKey(c => c.ProductId),
                        c => c.HasOne(c => c.Order)
                        .WithMany(c => c.OrderProducts)
                        .HasForeignKey(c => c.OrderId));
                });
            builder.Entity<Reviews>(
                c => {
                    c.HasOne(c => c.Client)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(c => c.ClientId);
                    c.HasOne(c => c.Product)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(c => c.ProductId);
                });
            builder.Entity<OrderReport>(
                c =>
                {
                    c.HasOne(c => c.Order)
                   .WithMany(c => c.OrderReports)
                   .HasForeignKey(c => c.OrderId);
                    c.HasOne(c => c.Reason)
                    .WithMany(c => c.OrderReports)
                    .HasForeignKey(c => c.ReasonId);
                });
            builder.Entity<ProductReport>(
                c =>
                {
                    c.HasOne(c => c.Product)
                    .WithMany(c => c.ProductReports)
                    .HasForeignKey(c => c.ProductId);
                    c.HasOne(c => c.Reason)
                    .WithMany(c => c.ProductReports)
                    .HasForeignKey(c => c.ReasonId);
                });
        }        
    }
}
