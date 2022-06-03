using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название категории")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductSpecifications> ProductSpecifications { get; set; }
    }

}
