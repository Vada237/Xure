using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace Xure.Data
{
    public class Prices
    {        
        public long Id { get; set; }
        public PriceHistory PriceHistory { get; set; }

        [Required(ErrorMessage = "Выберите цену")]
        public long PriceHistoryId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<Product> Products { get; set; }
    }
}
