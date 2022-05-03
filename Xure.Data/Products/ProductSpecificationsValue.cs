using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class ProductSpecificationsValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductSpecifications ProductSpecification { get; set; }
        public int ProductSpecificationsId { get; set; }
    }
}
