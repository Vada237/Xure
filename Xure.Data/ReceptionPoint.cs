using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class ReceptionPoint
    {
        public int id { get; set; }
        public string Address { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public List<Order> Orders { get; set; }
    }
}
