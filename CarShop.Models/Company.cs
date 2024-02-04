using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Название компании")]
        public string? Name { get; set; }
        [DisplayName("Адрес компании")]
        public string? StreetAddres { get; set; }
        [DisplayName("Город")]
        public string? City { get; set; }
        [DisplayName("Штат/регион")]    
        public string? State { get; set; }
        [DisplayName("Почтовый индекс")]
        public string? PostalCode { get; set; }
        [DisplayName("Телефонный номер")]
        public string? PhoneNumber { get; set; }
    }
}
