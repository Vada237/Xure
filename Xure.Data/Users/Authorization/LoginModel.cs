using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Неверный логин")]  
        [UIHint("Логин")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [UIHint("Пароль")]
        public string Password { get; set; }
    }
}
