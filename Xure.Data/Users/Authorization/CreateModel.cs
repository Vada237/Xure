using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class CreateModel
    {                
        [UIHint("Номер телефона")]
        [Required(ErrorMessage = "Укажите номер телефона")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [UIHint("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [UIHint("Фамилия")]
        public string Surname { get; set; }

        [UIHint("Отчество")]        
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Укажите дату рождения")]
        [UIHint("Дата рождения")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Укажите серию и номер пасспорта")]
        [UIHint("Серия и номер паспорта")]
        public string Passport { get; set; }
        public bool Confirmed { get; set; }

        [Required(ErrorMessage = "Добавьте аватар")]
        [FileExtensions(Extensions = "jpg,jpeg,png",ErrorMessage = "Поддерживаются только форматы: jpg, png, jpeg")]
        [UIHint("Аватар")]
        public byte[] Avatar { get; set; }
                
        [UIHint("Почта")]
        [Required(ErrorMessage = "Укажите почту")]
        public string Email { get; set; }
        
        [UIHint("Пароль")]
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
    }
}
