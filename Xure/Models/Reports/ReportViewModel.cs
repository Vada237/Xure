using Xure.Data;
using System.Collections.Generic;


namespace Xure.App.Models
{
    public class ReportViewModel
    {
        public IEnumerable<OrderReport> orderReports { get; set; } 
        
        public OrderReport OrderReport { get; set; }

        public OrderProduct OrderProduct { get; set; }
    }
}
