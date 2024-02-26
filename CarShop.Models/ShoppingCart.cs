using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }
        [Range(0,5, ErrorMessage = "Пожалуйста, введите значение в диапазоне от 1 до 5")]
        public int CountBasic { get; set; } // Количество выбранного товара (базовая комплектация)
        [Range(0, 5, ErrorMessage = "Пожалуйста, введите значение в диапазоне от 1 до 5")]
        public int CountFull { get; set; } // Количество выбранного товара (полная комплектация)

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [NotMapped]
        public double BasicEquipmentPrice { get; set; }
        [NotMapped]
        public double FullEquipmentPrice { get; set; }

    }
}
