using CarShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarShop.DataAccess.Data
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }


        public DbSet<Category> Categories { get; set; } // Это свойство необходимо для создания таблицы
                                                        // с типами кузовов авто в БД

        public DbSet<Brand> Brands { get; set; } // Это свойство необходимо для создания таблицы
                                                 // с названиями марок авто в БД

        public DbSet<Product> Products { get; set; } // Это свойство необходимо для создания таблицы
                                                     // с названиями моделей авто в БД

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, DisplayOrder = 1, Name = "Седан" },
                new Category { Id = 2, DisplayOrder = 2, Name = "Купе"},
                new Category { Id = 3, DisplayOrder = 3, Name = "Хэтчбек" },
                new Category { Id = 4, DisplayOrder = 4, Name = "Лифтбек" },
                new Category { Id = 5, DisplayOrder = 5, Name = "Фастбек" },
                new Category { Id = 6, DisplayOrder = 6, Name = "Универсал" },
                new Category { Id = 7, DisplayOrder = 7, Name = "Кроссовер" },
                new Category { Id = 8, DisplayOrder = 8, Name = "Внедорожник" },
                new Category { Id = 9, DisplayOrder = 9, Name = "Пикап" },
                new Category { Id = 10, DisplayOrder = 10, Name = "Легковой фургон" },
                new Category { Id = 11, DisplayOrder = 11, Name = "Минивен" },
                new Category { Id = 12, DisplayOrder = 12, Name = "Компактвен" },
                new Category { Id = 13, DisplayOrder = 13, Name = "Микровен" },
                new Category { Id = 14, DisplayOrder = 14, Name = "Кабриолет" },
                new Category { Id = 15, DisplayOrder = 15, Name = "Родстер" },
                new Category { Id = 16, DisplayOrder = 16, Name = "Тарга" },
                new Category { Id = 17, DisplayOrder = 17, Name = "Ландо" },
                new Category { Id = 18, DisplayOrder = 18, Name = "Лимузин" }
                );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, DisplayOrder = 1, Name = "Toyota"},
                new Brand { Id = 2, DisplayOrder = 2, Name = "Subaru"},
                new Brand { Id = 3, DisplayOrder = 3, Name = "BMW"},
                new Brand { Id = 4, DisplayOrder = 4, Name = "Mercedes" },
                new Brand { Id = 5, DisplayOrder = 5, Name = "Mitsubishi" },
                new Brand { Id = 6, DisplayOrder = 6, Name = "Nissan"},
                new Brand { Id = 7, DisplayOrder = 7, Name = "Opel"}, 
                new Brand { Id = 8, DisplayOrder = 8, Name = "Renault"},
                new Brand { Id = 9, DisplayOrder = 9, Name = "Honda"},
                new Brand { Id = 10, DisplayOrder = 10, Name = "Audi"},
                new Brand { Id = 11, DisplayOrder = 11, Name = "Volvo"},
                new Brand { Id = 12, DisplayOrder = 12, Name = "Kia"},
                new Brand { Id = 13, DisplayOrder = 13, Name = "Hyundai" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    CarName = "BMW 318i",
                    Description = "Мощность двигателя: 156 л.с\nРасход топлива: 6,3 л/100 км",
                    basicEquipmentPrice = 120000,
                    fullEquipmentPrice = 2000000,
                    CategoryId = 7,
                    BrandId = 3,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 2,
                    CarName = "BMW X6 M50i",
                    Description = "Мощность двигателя: 480 л.с.\nРасход топлива в л/100км (смешанный цикл): 11.5",
                    basicEquipmentPrice = 1000000,
                    fullEquipmentPrice = 180000,
                    CategoryId = 1,
                    BrandId = 3,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 3,
                    CarName = "Mercedes-Benz C118/X118",
                    Description = "Мощность двигателя: 421 л.с.\nРасход топлива: 9.2 на 100 км",
                    basicEquipmentPrice = 1500000,
                    fullEquipmentPrice = 2000000,
                    CategoryId = 6,
                    BrandId = 4,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 4,
                    CarName = "Nissan Tiida",
                    Description = "Мощность двигателя: 128 л.с.\nРасход топлива: 4.7-10.1 л/100 км",
                    basicEquipmentPrice = 700000,
                    fullEquipmentPrice = 850000,
                    CategoryId = 3,
                    BrandId = 6,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 5,
                    CarName = "Toyota Sequoia",
                    Description = "Мощность двигателя: 437 л.с.n\nРасход топлива: 11.7-23 л/100 км",
                    basicEquipmentPrice = 1200000,
                    fullEquipmentPrice = 1400000,
                    CategoryId = 8,
                    BrandId = 1,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 6,
                    CarName = "Kia XCeed",
                    Description = "Мощность двигателя: 200 л.с.n\nРасход топлива: 7.1 л/100 км",
                    basicEquipmentPrice = 600000,
                    fullEquipmentPrice = 750000,
                    CategoryId = 2,
                    BrandId = 12,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 7,
                    CarName = "Honda Civic 1.0",
                    Description = "Мощность двигателя: 129 л.с.n\nРасход топлива: 5.5 л/100 км",
                    basicEquipmentPrice = 600000,
                    fullEquipmentPrice = 750000,
                    CategoryId = 1,
                    BrandId = 9,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 8,
                    CarName = "HYUNDAI Tucson",
                    Description = "Мощность двигателя: 185 л.с.n\nРасход топлива: 5.4 л/100 км",
                    basicEquipmentPrice = 600000,
                    fullEquipmentPrice = 750000,
                    CategoryId = 7,
                    BrandId = 13,
                    ImageURL = ""
                }
                ) ;
        }
    }
}
