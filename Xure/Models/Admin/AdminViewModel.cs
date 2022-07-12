using Xure.Data;
using System.Collections.Generic;


namespace Xure.App.Models
{
    public class AdminViewModel
    {
        public AppUser Admin { get; set; }
        public List<Sellers> sellers { get; set; }
        public IEnumerable<OrderReport> orderReports { get; set; }
        public IEnumerable<ProductReport> productReports { get; set; }             
        public OrderReport orderReport { get; set; }
        public ProductReport productReport { get; set; }
        public IEnumerable<Units> Units { get; set; }

        public IEnumerable<ProductSpecifications> ProductSpecifications { get; set; }   

        public IEnumerable<ProductSpecificationsValue> ProductSpecificationsValues { get; set; }

    }
}
