using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Delivery
    {
        public long Id { get; set; }
        public ReceptionPoint ReceptionPoint { get; set; }
        [Required(ErrorMessage = "Укажите пункт выдачи")]
        public int ReceprtionPointId { get; set; }

        [Required(ErrorMessage = "Введите дату отправления")]
        [DataType(DataType.DateTime)]
        public DateTime DepartTime { get; set; }

        [Required(ErrorMessage = "Введите дату прибытия")]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        public List<SellerOrder> SellerOrders { get; set; }

    }
}
