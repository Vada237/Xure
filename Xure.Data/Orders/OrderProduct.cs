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
    }
}
