using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xure.Data
{
    public class Order
    {
        public long Id { get; set; }      
        
        [NotMapped]
        [BindNever]
        public ICollection<CartLine> CartLines { get; set; }    
        public Clients Client { get; set; }
        [Required(ErrorMessage = "Не указан клиент")]
        public int ClientId { get; set; }        
        public ReceptionPoint ReceptionPoint { get; set; }        

        [Required(ErrorMessage = "Укажите пункт выдачи")]
        public int ReceptionPointId { get; set; }        
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Отсутствует статус заказа")]
        [MaxLength(40,ErrorMessage = "Длина статуса не должна превышать 40 символов")]
        public string Status { get; set; }        
        public string TrackNumber { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<Product> Products { get; set; }        
        public List<OrderReport> OrderReports { get; set; }        
        public List<SellerOrder> SellerOrders { get; set; }
        
    }
}

