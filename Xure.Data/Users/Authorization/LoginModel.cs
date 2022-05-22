﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class LoginModel
    {
        [Required]  
        [UIHint("Логин")]
        public string Email { get; set; }

        [Required]
        [UIHint("Пароль")]
        public string Password { get; set; }
    }
}
