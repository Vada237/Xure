using System;
using System.Collections.Generic;

namespace Xure.Data
{
    public class Delivery
    {
        public long Id { get; set; }
        public Storage storage { get; set; }
        public byte StorageId { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public List<SellerOrder> SellerOrders { get; set; }

    }
}
