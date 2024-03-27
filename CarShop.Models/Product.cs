using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Название авто")]
        public string CarName { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Цена базовой комплектации")]
        [Range(1, 100000000)]
        public double basicEquipmentPrice { get; set; }

        [DisplayName("Цена полной комплектации")]
        [Range(1, 100000000)]
        public double fullEquipmentPrice { get; set; }

        // Внешний ключ для типа кузова
        
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        // Внешний ключ для марки автомобиля
      
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand Brand { get; set; }

        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }

    }
}
