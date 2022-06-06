using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class ProductViewModel
    {
        public CreateProductViewModel Product { get; set; }
        public Category Category { get; set; }
        public Brands Brand { get; set; }
        public PriceHistory PriceHistory { get; set; }
        public PageViewModel Page { get; set; }
        public IEnumerable<Brands> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }

    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Введите название товара")]
        [MinLength(3, ErrorMessage = "Минимальная длина товара не должна быть меньше 3 символов")]
        [MaxLength(100, ErrorMessage = "Минимальная длина товара не должна быть больше 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Опишите товар")]
        [MinLength(10, ErrorMessage = "Минимальное описание товара не должна быть меньше 10 символов")]
        [MaxLength(600, ErrorMessage = "Минимальное описание товара не должно быть больше 600 символов")]
        public string Description { get; set; }
        public Prices Price { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        public long PriceId { get; set; }        

        [Required(ErrorMessage = "Укажите бренд")]
        public int BrandId { get; set; }        

        [Required(ErrorMessage = "Укажите категорию товара")]
        public int CategoryId { get; set; }        

        [Required(ErrorMessage = "Укажите поставщика")]
        public int SellerId { get; set; }        

        [Required(ErrorMessage = "Укажите количество")]
        public int Count { get; set; }
        public IFormFile Image { get; set; }
    }
    
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Product? product { get; set; }        
        
        public IEnumerable<Brands> Brands { get; set; }
        public IEnumerable<ProductSpecifications> productSpecifications { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ProductSpecificationsValue> productSpecificationsValues { get; set; }
        
    }

    public class ProductInfoViewModel
    {
        public Product product { get; set; }
        public IEnumerable<ProductSpecifications> productSpecifications { get; set; }
        public ProductSpecifications productSpecification { get; set; }
        public Units Unit { get; set; }
        public IEnumerable<Units> Units { get; set; }

        public bool? locked { get; set; }
        public IEnumerable<ProductSpecificationsValue> productSpecificationsValues { get; set; }
        public ProductSpecificationsValue productSpecificationsValue { get; set; }                
    }
}
