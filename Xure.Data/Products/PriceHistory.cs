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
    public class PriceHistory
    {
        public long Id { get; set; }

        public Product Product { get; set; }                
        public long? ProductId { get; set; }
        
        [Required(ErrorMessage = "Укажите цену")]        
        public decimal Value { get; set; }
        
        [Required(ErrorMessage = "Дата обновления цены не добавлена")]
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<Prices> Prices { get; set; }

    }
}
