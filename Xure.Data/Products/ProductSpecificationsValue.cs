using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class ProductSpecificationsValue
    {
        public int Id { get; set; }        
        
        public ProductSpecifications ProductSpecification { get; set; }

        [Required(ErrorMessage = "Укажите характеристику")]
        public int ProductSpecificationsId { get; set; }

        public Product Product { get; set; }

        [Required(ErrorMessage = "Укажите продукт")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Введите значение характеристики")]
        [MaxLength(50,ErrorMessage = "Длина значения не должна превышать 50 символов")]
        public string Value { get; set; }
        public Units Unit { get; set; }
        [Required(ErrorMessage = "Введите единицу измерения")]
        public int UnitId { get; set; }

     }
}
