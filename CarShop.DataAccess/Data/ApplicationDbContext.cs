using CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }


        public DbSet<Category> Categories { get; set; } // Это свойство необходимо для создания таблицы в БД

        public DbSet<Brand> Brands { get; set; } // Это свойство необходимо для создания таблицы в БД

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
