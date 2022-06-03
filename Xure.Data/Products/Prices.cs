using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Prices
    {        
        public long Id { get; set; }
        public PriceHistory PriceHistory { get; set; }

        [Required(ErrorMessage = "Выберите цену")]
        public long PriceHistoryId { get; set; }
        public List<Product> Products { get; set; }
    }
}
