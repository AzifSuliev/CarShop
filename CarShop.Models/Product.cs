using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public decimal basicEquipmentPrice { get; set; }

        [DisplayName("Цена полной комплектации")]
        [Range(1, 100000000)]
        public decimal fullEquipmentPrice { get; set; }
    }
}
