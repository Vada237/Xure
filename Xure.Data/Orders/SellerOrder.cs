using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class SellerOrder
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public long OrderId { get; set; }
        public Delivery Delivery { get; set; }
        public long DeliveryId { get; set; }
    }
}
