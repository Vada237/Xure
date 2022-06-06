using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class SellerOrderViewModel
    {
        public IEnumerable<SellerOrder> SellerOrders { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
