using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; } // Первичный ключ
        [Required]
        public string? Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
