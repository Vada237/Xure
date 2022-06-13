using System.Collections.Generic;
using Xure.Data;


namespace Xure.App.Models
{
    public class reportOrderModel
    {
        public OrderProduct orderProduct { get; set; }

        public OrderReport orderReport { get; set; }
        public IEnumerable<Reason> Reasons { get; set; }
    }
}
