using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class PriceHistory
    {
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Укажите цену")]        
        public decimal Value { get; set; }
        
        [Required(ErrorMessage = "Дата обновления цены не добавлена")]
        public DateTime UpdatedDate { get; set; }

        public List<Prices> Prices { get; set; }

    }
}
