using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class ReceptionPoint
    {
        public int id { get; set; }               

        [Required(ErrorMessage = "Введите адрес")]
        [RegularExpression(@"^г.\w*,\w*.\w*,\d+$")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите время открытия")]
        [Timestamp]
        public TimeSpan OpenTime { get; set; }

        [Required(ErrorMessage = "Введите время закрытия")]
        [Timestamp]
        public TimeSpan CloseTime { get; set; }
        public List<Order> Orders { get; set; }
        public List<Delivery> Deliveries   { get; set; }
    }
}
