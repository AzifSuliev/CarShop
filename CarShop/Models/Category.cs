using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; } // Первичный ключ
        [Required]
        [DisplayName("Category Name")] // Название для поля ввода 
        [MaxLength(30)] // Ограничение в 30 символов
        public string? Name { get; set; }

        [DisplayName("Display Order")] // Название для поля ввода 
        [Range(1,100, ErrorMessage = "Значение вне диапазона!")] // Значения int от 1 до 100
        public int DisplayOrder { get; set; }
    }
}
