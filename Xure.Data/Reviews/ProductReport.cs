using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class ProductReport
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        [Required(ErrorMessage = "Не выбран продукт")]
        public long ProductId { get; set; }        
        public Reason Reason { get; set; }

        [Required(ErrorMessage = "Выберите причину")]
        public byte ReasonId { get; set; }
        public string Commentary { get; set; }
    }
}
