using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class ProductSpecifications
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите характеристику")]
        [MinLength(3, ErrorMessage = "Название характеристики не должно быть меньше 3 символов")]
        [MaxLength(60, ErrorMessage = "Название характеристики не должно превыщать 60 символов")]
        public string Name { get; set; }
        public Category Category { get; set; }  
        
        [Required(ErrorMessage = "Укажите категорию")]
        public int CategoryId { get; set; }

        public List<ProductSpecificationsValue> ProductSpecificationsValues { get; set; }
    }
}
