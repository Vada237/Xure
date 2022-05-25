using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class RoleModel  {

        [Required]
        [MinLength(5,ErrorMessage = "Длина роли не должна быть меньше 5 символов")]
        [MaxLength(256, ErrorMessage = "Длина роли не должна превышать 256 символов")]
        public string Name { get; set; }        
    }

}
