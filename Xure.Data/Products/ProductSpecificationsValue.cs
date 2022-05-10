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
        
        public ProductSpecifications ProductSpecification { get; set; }
        public int ProductSpecificationsId { get; set; }

        public Product Product { get; set; }

        public long ProductId { get; set; }
        public string Value { get; set; }
        public Units Unit { get; set; }

        public int UnitId { get; set; }

     }
}
