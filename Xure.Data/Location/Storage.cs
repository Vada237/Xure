using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Storage
    {
        public byte id { get; set; }
        public string Address { get; set; }        
        public List<Delivery> Deliveries { get; set; }
    }
}
