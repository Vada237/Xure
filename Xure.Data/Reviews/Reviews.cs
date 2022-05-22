using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Reviews
    {
        public long Id { get; set; }

        public Clients Client;

        [Required(ErrorMessage = "Не указан клиент")]        
        public int ClientId { get; set; }
        public Product Product {get;set;}
        [Required(ErrorMessage = "Не указан продукт")]
        public long ProductId { get; set; }
        public byte Rating { get; set; }
        public string Commentary { get; set; }
    }
}
