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
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } // Внешний ключ
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } // Навигационное свойство

        public DateTime OrderDate { get; set; } // Дата оформления заказа
        public DateTime ShippingDate { get; set; } // Дата отгрузки
        public double OrderTotal { get; set; } // Сумма заказа

        public string? OrderStatus { get; set; } // Статус заказа
        public string? PaymentStatus { get; set; } // Статус оплаты
        public string? TrackingNumber { get; set; } // Номер для отслеживания товара
        public string? Carrier { get; set; } // Перевозчик

        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntendId { get; set; }

        // Ниже свойства для данных покупателя
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? StreetAddress { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}
