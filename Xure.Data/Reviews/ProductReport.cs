using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class ProductReport
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }        
        public Reason Reason { get; set; }
        public byte ReasonId { get; set; }
        public string Commentary { get; set; }
    }
}
