using Xure.Data;
using System.Collections.Generic;


namespace Xure.App.Models
{
    public class CategoryModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public Category Category { get; set; }
    }
}
