using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Xure.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public DbSet<Brands> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderReport> OrderReports { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReport> ProductReports { get; set; }
        public DbSet<ProductSpecifications> ProductSpecifications { get; set; }
        public DbSet<ProductSpecificationsValue> ProductSpecificationsValues { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<ReceptionPoint> ReceptionPoints { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Sellers> Sellers { get; set; }


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
                c.HasOne(c => c.Brands)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.BrandId);
                c.HasOne(c => c.Price)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.PriceId);
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
                    c.HasOne(c => c.Storage)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(c => c.StorageId);
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
            builder.Entity<ProductSpecifications>(
                c => c.HasOne(c => c.Category)
                .WithMany(C => C.ProductSpecifications)
                .HasForeignKey(c => c.CategoryId));
            builder.Entity<ProductSpecificationsValue>(
                c => c.HasOne(c => c.ProductSpecification)
                .WithMany(c => c.ProductSpecificationsValues)
                .HasForeignKey(c => c.ProductSpecificationsId));
                
        }        
    }
}
