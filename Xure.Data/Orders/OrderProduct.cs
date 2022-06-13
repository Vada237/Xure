using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class OrderProduct
    {
        public Product Product { get; set; }
        
        [Required(ErrorMessage = "Не добавлен продукт")]
        public long ProductId { get; set; }
        public Order Order { get; set; }

        [Required(ErrorMessage = "Не добавлен заказ")]
        public long OrderId { get; set; }

        [Required(ErrorMessage = "Не указано количество")]
        public int Quantity { get; set; }

        public ReceptionPoint ReceptionPoint { get; set; }

        [Required(ErrorMessage = "Укажите пункт выдачи")]
        public int ReceptionPointId { get; set; }
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Отсутствует статус заказа")]
        [MaxLength(40, ErrorMessage = "Длина статуса не должна превышать 40 символов")]
        public string Status { get; set; }
        public string TrackNumber { get; set; }        

    }
}
