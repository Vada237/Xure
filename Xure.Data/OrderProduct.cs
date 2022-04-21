using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class OrderProduct
    {
        public Product Product { get; set; }

        public long ProductId { get; set; }
        public Order Order { get; set; }
        public long OrderId { get; set; }
        
    }
}
