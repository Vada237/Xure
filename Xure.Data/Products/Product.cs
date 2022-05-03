using System;
using System.Collections.Generic;

namespace Xure.Data
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Prices Price { get; set; }
        public long PriceId { get; set; }
        public Brands Brands { get; set; }
        public int BrandId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }        
        public Sellers Seller { get; set; }
        public string SellerId { get; set; }        
        public string Image { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<Reviews> Reviews { get; set; }        
        public List<ProductReport> ProductReports { get; set; }
        

    }
}
