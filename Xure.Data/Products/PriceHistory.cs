using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class PriceHistory
    {
        public long Id { get; set; }
        public decimal Value { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<Prices> Prices { get; set; }

    }
}
