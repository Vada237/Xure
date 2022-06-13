using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class ProductMainViewModel
    {
        public IEnumerable<Product> MostPopular { get; set; }

        public IEnumerable<Product> NewProducts { get; set; }

        public IEnumerable<Product> HighRating { get; set; }

        public IEnumerable <Category> Categories { get; set; }

        public IEnumerable<Brands> Brands { get; set; }
    
        public IEnumerable<Product> AllProducts { get; set; }
    }
}
