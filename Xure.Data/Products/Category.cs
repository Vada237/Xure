using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Xure.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название категории")]
        public string Name { get; set; }

        [IgnoreDataMember]
        public List<Product> Products { get; set; }
        [IgnoreDataMember]
        public List<ProductSpecifications> ProductSpecifications { get; set; }
    }

}
