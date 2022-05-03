using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Prices
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Product> Products { get; set; }
    }
}
