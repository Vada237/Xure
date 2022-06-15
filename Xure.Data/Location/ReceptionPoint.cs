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
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите время открытия")]        
        public TimeSpan? OpenTime { get; set; }

        [Required(ErrorMessage = "Введите время закрытия")]        
        public TimeSpan? CloseTime { get; set; }
        
        public List<OrderProduct> OrderProducts { get; set; }
        public List<Delivery> Deliveries   { get; set; }
    }
}
