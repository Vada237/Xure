using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class ProductSpecificationsModel
    {
        public IEnumerable<ProductSpecifications> ProductSpecifications { get; set; }

        public ProductSpecifications ProductSpecification { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
