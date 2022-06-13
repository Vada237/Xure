using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class ReportProductViewModel
    {
        public IEnumerable<Reason> Reasons { get; set; }
        public ProductReport ProductReport { get; set; }

        public Product Product { get; set; }
    }
}
