using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

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
        public DbSet<Sellers> Sellers { get; set; }
        public DbSet<SellerOrder> SellerOrders { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }


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

            builder.Entity<Prices>(
                c => c.HasOne(c => c.PriceHistory)
                .WithMany(c => c.Prices)
                .HasForeignKey(c => c.PriceHistoryId)
                );

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
            builder.Entity<ProductSpecifications>(
                c =>
                {
                    c.HasKey(c => c.Id);
                    c.HasOne(c => c.Category)
                    .WithMany(C => C.ProductSpecifications)
                    .HasForeignKey(c => c.CategoryId);
                });
            builder.Entity<ProductSpecificationsValue>(
                c => {
                    c.HasOne(c => c.ProductSpecification)
                    .WithMany(c => c.ProductSpecificationsValues)
                .HasForeignKey(c => c.ProductSpecificationsId);
                    c.HasOne(c => c.Unit)
                    .WithMany(c => c.productSpecificationsValues)
                    .HasForeignKey(c => c.UnitId);
                    c.HasOne(c => c.Product)
                    .WithMany(c => c.ProductSpecificationsValues)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);
                }
                );
            builder.Entity<Sellers>(
                c => {
                    c.HasOne(c => c.UserInfo)
                   .WithOne(c => c.Seller)
                   .HasForeignKey<Sellers>(c => c.UserId);
                });
            builder.Entity<Clients>(
                c => {
                    c.HasOne(c => c.UserInfo)
                    .WithOne(c => c.Client)
                    .HasForeignKey<Clients>(c => c.UserId);
                });
            builder.Entity<SellerOrder>(
                c =>
                {
                    c.HasOne(c => c.Delivery)
                .WithMany(c => c.SellerOrders)
                .HasForeignKey(c => c.DeliveryId);
                    c.HasOne(c => c.Order)
                    .WithMany(c => c.SellerOrders)
                    .HasForeignKey(c => c.OrderId);
                });
            builder.Entity<Delivery>(
                c => c.HasOne(c => c.ReceptionPoint)
                .WithMany(c => c.Deliveries)
                .HasForeignKey(c => c.ReceprtionPointId)
                .OnDelete(DeleteBehavior.NoAction));
            builder.Entity<PriceHistory>(
                c => c.HasOne(c => c.Product)
                .WithMany(c => c.PriceHistories)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction)
                );
            //// Первичная инициализация данными


            builder.Entity<Category>().HasData(
                new Category[] {
                    new Category { Id = 1, Name = "Продукты питания"},
                    new Category { Id = 2, Name = "Одежда"},
                    new Category { Id = 3, Name = "Техника"},
                    new Category { Id = 4, Name = "Спорт"},
                    new Category { Id = 5, Name = "Образование" },
                    new Category { Id = 6, Name = "Бытовые товары" },
                    new Category { Id = 7, Name = "Медицина" },
                    new Category { Id = 8, Name = "Мебель"},
                    new Category { Id = 9, Name = "Аксессуары"}
                });

            builder.Entity<Units>().HasData(
                new Units[]
                {
                    new Units { id = 1, Name = "Г" },
                    new Units { id = 2, Name = "Кг"},
                    new Units { id = 3, Name = "Мл"},
                    new Units { id = 4, Name = "Л"},
                    new Units { id = 5, Name = "См"},
                    new Units { id = 6, Name = "М"},
                    new Units { id = 7, Name = "Шт"},
                    new Units { id = 8, Name = "Упаковка"},
                    new Units { id = 9, Name = "mA"},
                    new Units { id = 10, Name = "Хлопок"},
                    new Units { id = 11, Name = "Лён"},
                    new Units { id = 12, Name = "Шелк"},
                    new Units { id = 13, Name = "Шерсть"},
                    new Units { id = 14, Name = "Вискоза"},
                    new Units { id = 15, Name = "RU"},
                    new Units { id = 16, Name = "EU"},
                    new Units { id = 17, Name = "С"},
                    new Units { id = 18, Name = "В"},
                    new Units { id = 19, Name = "Мм"},
                });

            builder.Entity<ProductSpecifications>().HasData(
                new ProductSpecifications[]
                {
                    new ProductSpecifications { Id = 1, Name = "Вес", CategoryId = 1},
                    new ProductSpecifications { Id = 2, Name = "Вес", CategoryId = 2},
                    new ProductSpecifications { Id = 3, Name = "Вес", CategoryId = 3},
                    new ProductSpecifications { Id = 4, Name = "Вес", CategoryId = 4},
                    new ProductSpecifications { Id = 5, Name = "Вес", CategoryId = 5},
                    new ProductSpecifications { Id = 6, Name = "Вес", CategoryId = 6},
                    new ProductSpecifications { Id = 7, Name = "Вес", CategoryId = 7},
                    new ProductSpecifications { Id = 8, Name = "Вес", CategoryId = 8},
                    new ProductSpecifications { Id = 9, Name = "Вес", CategoryId = 9},
                    new ProductSpecifications { Id = 10, Name = "Тип", CategoryId = 1},
                    new ProductSpecifications { Id = 11, Name = "Тип", CategoryId = 2},
                    new ProductSpecifications { Id = 12, Name = "Тип", CategoryId = 3},
                    new ProductSpecifications { Id = 13, Name = "Тип", CategoryId = 4},
                    new ProductSpecifications { Id = 14, Name = "Тип", CategoryId = 5},
                    new ProductSpecifications { Id = 15, Name = "Тип", CategoryId = 6},
                    new ProductSpecifications { Id = 16, Name = "Тип", CategoryId = 7},
                    new ProductSpecifications { Id = 17, Name = "Тип", CategoryId = 8},
                    new ProductSpecifications { Id = 18, Name = "Тип", CategoryId = 9},
                    new ProductSpecifications { Id = 19, Name = "Пищевая ценность", CategoryId = 1},
                    new ProductSpecifications { Id = 20, Name = "Энергетическая ценность", CategoryId = 1},
                    new ProductSpecifications { Id = 21, Name = "Материал", CategoryId = 2},
                    new ProductSpecifications { Id = 22, Name = "Материал", CategoryId = 3},
                    new ProductSpecifications { Id = 23, Name = "Цвет", CategoryId = 2},
                    new ProductSpecifications { Id = 24, Name = "Цвет", CategoryId = 3},
                    new ProductSpecifications { Id = 25, Name = "Цвет", CategoryId = 4},
                    new ProductSpecifications { Id = 26, Name = "Объем", CategoryId = 3},
                    new ProductSpecifications { Id = 27, Name = "Количество в упаковке", CategoryId = 1},
                    new ProductSpecifications { Id = 28, Name = "Количество в упаковке", CategoryId = 2},
                    new ProductSpecifications { Id = 29, Name = "Количество в упаковке", CategoryId = 3},
                    new ProductSpecifications { Id = 30, Name = "Количество в упаковке", CategoryId = 4},
                    new ProductSpecifications { Id = 31, Name = "Количество в упаковке", CategoryId = 5},
                    new ProductSpecifications { Id = 32, Name = "Количество в упаковке", CategoryId = 6},
                    new ProductSpecifications { Id = 33, Name = "Количество в упаковке", CategoryId = 7},
                    new ProductSpecifications { Id = 34, Name = "Количество в упаковке", CategoryId = 8},
                    new ProductSpecifications { Id = 35, Name = "Количество в упаковке", CategoryId = 9},
                    new ProductSpecifications { Id = 36, Name = "Напряжение", CategoryId = 3},
                    new ProductSpecifications { Id = 37, Name = "Длина", CategoryId = 4},
                    new ProductSpecifications { Id = 38, Name = "Материал", CategoryId = 4},
                    new ProductSpecifications { Id = 39, Name = "Издательство", CategoryId = 5},
                    new ProductSpecifications { Id = 40, Name = "Год выпуска", CategoryId = 5},
                    new ProductSpecifications { Id = 41, Name = "Обьем", CategoryId = 6},
                    new ProductSpecifications { Id = 42, Name = "Размеры, мм", CategoryId = 7},
                    new ProductSpecifications { Id = 43, Name = "Область применения", CategoryId = 7},
                    new ProductSpecifications { Id = 44, Name = "Ширина", CategoryId = 8},
                    new ProductSpecifications { Id = 45, Name = "Глубина", CategoryId = 8},
                    new ProductSpecifications { Id = 46, Name = "Высота", CategoryId = 8},
                    new ProductSpecifications { Id = 47, Name = "Материал", CategoryId = 9},
                });
            builder.Entity<ReceptionPoint>().HasData(
                new ReceptionPoint[]
                {
                    new ReceptionPoint { id = 1, Address = "Москва, ул.Лестева, д.9", OpenTime = new TimeSpan(7,30,30), CloseTime = new TimeSpan(20,30,00)},
                    new ReceptionPoint { id = 2, Address = "Воронеж, ул.3 Интернационала, д.35", OpenTime = new TimeSpan(8,30,30), CloseTime = new TimeSpan(20,30,00)},
                    new ReceptionPoint { id = 3, Address = "Ростов-на-Дону, пер. Журавлева, д.127", OpenTime = new TimeSpan(8,30,30), CloseTime = new TimeSpan(20,30,00)},
                    new ReceptionPoint { id = 4, Address = "Ставрополь, ул. Ломоносова, д.30", OpenTime = new TimeSpan(7,30,30), CloseTime = new TimeSpan(20,30,00)},
                });
        }

        public static async Task CreateAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                      
                AppUser user = new AppUser
                {
                    UserName = configuration["UserData:Admin:Name"],
                    Surname = configuration["UserData:Admin:Surname"],
                    PhoneNumber = configuration["UserData:Admin:PhoneNumber"],
                    Birthday = Convert.ToDateTime(configuration["UserData:Admin:Birthday"]),
                    Passport = configuration["UserData:Admin:Passport"],
                    Email = configuration["UserData:Admin:Email"],
                    
                };
                IdentityResult result = await userManager.CreateAsync(user, configuration["UserData:Admin:Password"]);
                            
            if (await roleManager.FindByNameAsync("Администратор") == null)
            { 
                result = await roleManager.CreateAsync(new IdentityRole("Администратор"));
                result = await roleManager.CreateAsync(new IdentityRole("Покупатель"));
                result = await roleManager.CreateAsync(new IdentityRole("Поставщик"));
                result = await roleManager.CreateAsync(new IdentityRole("НеподтвержденныйПоставщик"));
                result = await roleManager.CreateAsync(new IdentityRole("Модератор"));
                result = await userManager.AddToRoleAsync(user, "Администратор");
            }           
        }
    }
}
