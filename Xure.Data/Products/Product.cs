using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Xure.Data
{
    public class Product
    {
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Введите название товара")]
        [MinLength(3,ErrorMessage = "Минимальная длина товара не должна быть меньше 3 символов")]
        [MaxLength(100, ErrorMessage = "Минимальная длина товара не должна быть больше 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Опишите товар")]
        [MinLength(10, ErrorMessage = "Минимальное описание товара не должна быть меньше 10 символов")]
        [MaxLength(600, ErrorMessage = "Минимальное описание товара не должно быть больше 600 символов")]
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

        [Required(ErrorMessage = "Укажите количество")]
        public int Сount { get; set; }

        [Required(ErrorMessage = "Укажите поставщика")]
        public int SellerId { get; set; }                           
        public byte[] Image { get; set; }

        public List<Order> Orders { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<Reviews> Reviews { get; set; }        
        public List<ProductReport> ProductReports { get; set; }
        
        public List<PriceHistory> PriceHistories { get; set; }
        public List<ProductSpecificationsValue> ProductSpecificationsValues { get; set; }
    }
}
