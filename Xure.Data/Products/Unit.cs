using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Units
    {
        public int id { get; set; }
        public string Name { get; set; }

        public List<ProductSpecificationsValue> productSpecificationsValues { get; set; }
    }
}
