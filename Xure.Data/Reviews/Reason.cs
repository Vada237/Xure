using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Reason
    {
        public byte Id { get; set; }

        [Required(ErrorMessage = "Добавьте название причины")]
        [MinLength(5, ErrorMessage = "Название не должно быть меньше 5 символов")]
        [MaxLength(50,ErrorMessage = "Название не должно превышать 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Добавьте описание")]
        [MinLength(20,ErrorMessage = "Описание не должно быть меньше 20 символов")]
        [MaxLength(200, ErrorMessage = "Название не должно превышать 200 символов")]
        public string Description { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Причина не должна быть длиннее 20 символов")]
        public string Category { get; set; }
        public List<OrderReport> OrderReports { get; set; }

        public List<ProductReport> ProductReports { get; set; }
    }
}
