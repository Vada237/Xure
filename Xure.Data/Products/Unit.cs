using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Units
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Введите название единицы измерения")]
        [MaxLength(60,ErrorMessage = "Длина не должна превышать 60 символов")]
        public string Name { get; set; }

        public List<ProductSpecificationsValue> productSpecificationsValues { get; set; }
    }
}
