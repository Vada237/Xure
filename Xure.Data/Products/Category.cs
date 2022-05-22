using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название категории")]
        [MinLength(3,ErrorMessage = "Название категории не должны быть меньше 3 символов")]
        [MaxLength(50,ErrorMessage = "Название категории не должны превышать 50 символов")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductSpecifications> ProductSpecifications { get; set; }
    }

}
