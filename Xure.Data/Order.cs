using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Order
    {
        public long Id { get; set; }
        public AppUser Client { get; set; }
        public string ClientId { get; set; }        
        public ReceptionPoint ReceptionPoint { get; set; }        
        public int ReceptionPointId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<Product> Products { get; set; }
        public List<OrderReport> OrderReports { get; set; }
    }
}

