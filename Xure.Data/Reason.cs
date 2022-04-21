using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Reason
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<OrderReport> OrderReports { get; set; }

        public List<ProductReport> ProductReports { get; set; }
    }
}
