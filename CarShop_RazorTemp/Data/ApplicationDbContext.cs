using CarShop_RazorTemp.Models.CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop_RazorTemp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; } // Это свойство необходимо для создания таблицы в БД

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, DisplayOrder = 1, Name = "Седан"},
                new Category { Id = 2, DisplayOrder = 2, Name = "Купе" },
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
        }
    }
}
