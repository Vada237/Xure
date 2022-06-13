using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class SellerOrderViewModel
    {
        public IEnumerable<SellerOrder> SellerOrders { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        
        public OrderProduct Product { get; set; }
        public Order order { get; set; }
        public SellerOrder sellerOrder { get; set; }   

        public Delivery Delivery { get; set; }
    }
}
