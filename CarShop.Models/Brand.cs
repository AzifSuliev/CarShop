using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [DisplayName("Display Order")] // Название для поля ввода 
        [Range(1, 100, ErrorMessage = "Значение вне диапазона!")] // Значения int от 1 до 100
        public int DisplayOrder { get; set; }
    }
}
