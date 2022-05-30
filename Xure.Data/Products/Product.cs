using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Xure.Data
{
    public class Product
    {
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Введите название товара")]
        [Range(5,100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Опишите товар")]
        [Range(10,600)]
        public string Description { get; set; }
        public Prices Price { get; set; }
        
        [Required(ErrorMessage = "Укажите цену")]        
        public long PriceId { get; set; }
        public Brands Brands { get; set; }

        [Required(ErrorMessage = "Укажите бренд")]
        public int BrandId { get; set; }
        public Category Category { get; set; }

        [Required(ErrorMessage = "Укажите категорию товара")]
        public int CategoryId { get; set; }        
        public Sellers Seller { get; set; }

        [Required(ErrorMessage = "Укажите поставщика")]
        public int SellerId { get; set; }   
        
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Изображения поддерживаются только в формате jpeg,jpg и png")]       
        public byte[] Image { get; set; }

        public List<Order> Orders { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<Reviews> Reviews { get; set; }        
        public List<ProductReport> ProductReports { get; set; }
        
        public List<ProductSpecificationsValue> ProductSpecificationsValues { get; set; }
    }
}
