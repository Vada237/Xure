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
                c => c.HasOne(c => c.Category)
                .WithMany(C => C.ProductSpecifications)
                .HasForeignKey(c => c.CategoryId));
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

            // Первичная инициализация данными

            builder.Entity<Units>().HasData(
                new Units[] {
                    new Units { id = 1, Name = "Грамм"},
                    new Units { id = 2, Name = "Килограмм"},
                    new Units { id = 3, Name = "Литр"},
                    new Units { id = 4, Name = "Миллиметр"},
                    new Units { id = 5, Name = "Месяц"},
                    new Units { id = 6, Name = "Хлопок"},
                    new Units { id = 7, Name = "S"},
                    new Units { id = 8, Name = "Год"},
                    new Units {id = 9, Name = "mA" },
                    new Units { id = 10 , Name = " "}
                 });
            builder.Entity<Product>().HasData(
                new Product[] {
                    new Product { Id = 1, Name = "Яблоко", Description = "Зеленые Краснодарские яблоки", PriceId = 1, BrandId = 1, SellerId = 1, CategoryId = 1},
                    new Product { Id = 2, Name = "Джинсы", Description = "Недорогие джинсы", PriceId = 2, BrandId = 2, SellerId = 2, CategoryId = 2},
                    new Product { Id = 3, Name = "Рубашка", Description = "Недорогая рубашка", PriceId = 3, BrandId = 3, SellerId = 2, CategoryId = 2},
                    new Product { Id = 4, Name = "Сок", Description = "Вкусный яблочный сок без химии и добавок", PriceId = 4, BrandId = 4, SellerId = 1, CategoryId = 1},
                    new Product { Id = 5, Name = "Батарейки", Description = "Долгосрочные батарейки", PriceId = 5, BrandId = 5, SellerId = 2, CategoryId = 3}
                });
            builder.Entity<Prices>().HasData(
                new Prices[]
                {
                    new Prices { Id = 1, PriceHistoryId = 1},
                    new Prices { Id = 2, PriceHistoryId = 2},
                    new Prices { Id = 3, PriceHistoryId = 3},
                    new Prices { Id = 4, PriceHistoryId = 4},
                    new Prices { Id = 5, PriceHistoryId = 5}
                });
            builder.Entity<PriceHistory>().HasData(
                new PriceHistory[] { 
                    new PriceHistory { Id = 1, Value = 50, UpdatedDate = Convert.ToDateTime("2022-05-21T19:21")},
                    new PriceHistory { Id = 2, Value = 1849, UpdatedDate = Convert.ToDateTime("2022-03-18T14:35")},
                    new PriceHistory { Id = 3, Value = 1448, UpdatedDate = Convert.ToDateTime("2021-10-12T19:30")},
                    new PriceHistory { Id = 4, Value = 81, UpdatedDate = Convert.ToDateTime("2022-03-03T11:15")},
                    new PriceHistory { Id = 5, Value = 9, UpdatedDate = Convert.ToDateTime("2022-04-28T20:00")}
                });
            builder.Entity<Brands>().HasData(
                new Brands[] {
                    new Brands { Id = 1, Name = "Villams", Country = "Америка"},
                    new Brands { Id = 2, Name = "Tommy Hilfiger", Country = "Америка"},
                    new Brands { Id = 3, Name = "Polo", Country = "Великобритания"},
                    new Brands { Id = 4, Name = "Добрый", Country = "Россия"},
                    new Brands { Id = 5, Name = "Duracell", Country = "Германия"}
                });
            builder.Entity<Category>().HasData(
                new Category[] { 
                    new Category { Id = 1, Name = "Еда"},
                    new Category { Id = 2, Name = "Одежда"},
                    new Category { Id = 3, Name = "Техника"}
                });
            builder.Entity<ProductSpecifications>().HasData(
                new ProductSpecifications[] { 
                    new ProductSpecifications { Id = 1, Name = "Вес", CategoryId = 1},
                    new ProductSpecifications { Id = 2, Name = "Вес", CategoryId = 2},
                    new ProductSpecifications { Id = 3, Name = "Вес", CategoryId = 3},
                    new ProductSpecifications { Id = 4, Name = "Cостав", CategoryId = 1},
                    new ProductSpecifications { Id = 5, Name = "Cрок годности", CategoryId = 2},
                    new ProductSpecifications { Id = 6, Name = "Ткань", CategoryId = 2},
                    new ProductSpecifications { Id = 7, Name = "Размер", CategoryId = 2},
                    new ProductSpecifications { Id = 8, Name = "Мощность", CategoryId = 3}
                });
            builder.Entity<ProductSpecificationsValue>().HasData(
                new ProductSpecificationsValue[]
                {
                    new ProductSpecificationsValue { Id = 1, ProductId = 1, ProductSpecificationsId = 1, Value = "150", UnitId = 1},
                    new ProductSpecificationsValue { Id = 2, ProductId = 1, ProductSpecificationsId = 5, Value = "1", UnitId = 5},
                    new ProductSpecificationsValue { Id = 3, ProductId = 2, ProductSpecificationsId = 2, Value = "150", UnitId = 1},
                    new ProductSpecificationsValue { Id = 4, ProductId = 2, ProductSpecificationsId = 5, Value = "2", UnitId = 8},
                    new ProductSpecificationsValue { Id = 5, ProductId = 2, ProductSpecificationsId = 6, Value = "80%", UnitId = 6},
                    new ProductSpecificationsValue { Id = 6, ProductId = 2, ProductSpecificationsId = 7, Value = "44-46", UnitId = 7},
                    new ProductSpecificationsValue { Id = 7, ProductId = 3, ProductSpecificationsId = 2, Value = "135", UnitId = 1},
                    new ProductSpecificationsValue { Id = 8, ProductId = 3, ProductSpecificationsId = 5, Value = "16", UnitId = 5},
                    new ProductSpecificationsValue { Id = 9, ProductId = 3, ProductSpecificationsId = 6, Value = "100%", UnitId = 6},
                    new ProductSpecificationsValue { Id = 10, ProductId = 3, ProductSpecificationsId = 7, Value = "43-45", UnitId = 7},
                    new ProductSpecificationsValue { Id = 11, ProductId = 4, ProductSpecificationsId = 1, Value = "800", UnitId = 1},
                    new ProductSpecificationsValue { Id = 12, ProductId = 4, ProductSpecificationsId = 4, Value = "Яблочный концентрат", UnitId = 10},
                    new ProductSpecificationsValue { Id = 13, ProductId = 4, ProductSpecificationsId = 4, Value = "Вода", UnitId = 10},
                    new ProductSpecificationsValue { Id = 14, ProductId = 4, ProductSpecificationsId = 4, Value = "Cахар", UnitId = 10},
                    new ProductSpecificationsValue { Id = 15, ProductId = 4, ProductSpecificationsId = 5, Value = "6", UnitId = 5},
                    new ProductSpecificationsValue { Id = 16, ProductId = 5, ProductSpecificationsId = 3, Value = "30", UnitId = 1},
                    new ProductSpecificationsValue { Id = 17, ProductId = 5, ProductSpecificationsId = 5, Value = "10", UnitId = 8},
                    new ProductSpecificationsValue { Id = 18, ProductId = 5, ProductSpecificationsId = 8, Value = "2000", UnitId = 9}
                });
            builder.Entity<AppUser>().HasData(
                new AppUser[]
                {
                    new AppUser { Id = "c49e0b3a-bebc-47d3-b65d-e2da531830ae", UserName = "Darya", Surname = "Dubova", Email = "DaryaDubova@mail.ru",Passport = "6034 877186", PhoneNumber = "+7916463121"},
                    new AppUser { Id = "6a522bd3-d3d6-4f6c-bd0e-f00e38a89b86", UserName = "Daniil", Surname = "Petrov", Email = "DaniilPetrov@gmail.com", Passport = "2016 518374", PhoneNumber = "+7921649797"},
                    new AppUser { Id = "1ab2bff8-07be-441b-82d6-0d91174ad815", UserName = "Yana", Surname = "Levchenkova", Email = "YanaL@mail.ru", Passport = "6048 518375", PhoneNumber = "+79892221468"},
                    new AppUser { Id = "221a163b-8960-42f9-a19e-023493311599", UserName = "Yan", Surname = "Pedrechuk", Email = "YP@mail.ru", Passport = "6510 838162", PhoneNumber = "78106964233"}
                });
            builder.Entity<Company>().HasData(
                new Company[]
                {
                    new Company { Id = 1, Name = "iVan Clothes",
                        Description = "Американский магазин одежды премиум-класса, выпускающий одежду, обувь, аксессуары, ароматы и товары для дома.",
                            DateRegistration = Convert.ToDateTime("2020-11-04T14:41"), INN = "4534239794", OGRN = "1091363440190"},
                    new Company { Id = 2, Name = "Магнит", Description = "Сеть розничных магазинов", DateRegistration = Convert.ToDateTime("2021-12-26T21:14"),
                    INN = "0998416485", OGRN = "9057136863819"}
                });
            builder.Entity<Sellers>().HasData(
                new Sellers[]
                {
                    new Sellers { Id = 1, UserId = "c49e0b3a-bebc-47d3-b65d-e2da531830ae", CompanyId = 1},
                    new Sellers { Id = 2, UserId = "6a522bd3-d3d6-4f6c-bd0e-f00e38a89b86", CompanyId = 2}
                });
            builder.Entity<Clients>().HasData(
                new Clients[] {
                    new Clients { Id = 1, UserId = "1ab2bff8-07be-441b-82d6-0d91174ad815"},
                    new Clients { Id = 2, UserId = "221a163b-8960-42f9-a19e-023493311599"}
                });
        }

        public static async Task CreateAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (await userManager.FindByEmailAsync(configuration["UserData:Admin:Email"]) == null)
            {
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
                result = await roleManager.CreateAsync(new IdentityRole("Администратор"));
                result = await userManager.AddToRoleAsync(user, "Администратор");
            }
        }
    }
}
