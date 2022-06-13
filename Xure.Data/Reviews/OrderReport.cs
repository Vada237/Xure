using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class OrderReport
    {      
        public int Id { get; set; }
        public Order Order { get; set; }
        public long OrderId { get; set; }        
        public Product Product { get; set; }
        public long ProductId { get; set; } 
        public Reason Reason { get; set; }

        [Required(ErrorMessage = "Выберите причину")]
        public byte ReasonId { get; set; }
        public string Commentary { get; set; }


    }
}
