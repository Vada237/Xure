using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Company
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Введите название магазина")]
        [MaxLength(100,ErrorMessage = "Название компании не должна превышать 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание магазина")]
        [MinLength(30,ErrorMessage = "Описание магазина не должно быть меньше 30 символов")]
        [MaxLength(200, ErrorMessage = "Описание магазина не должно превышать 200 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите дату регистрации магазина")]
        public DateTime DateRegistration { get; set; }

        [Required(ErrorMessage = "Введите ИНН")]
        [MaxLength (20, ErrorMessage = "Длина ИНН не должна превышать 20 символов")]
        public string INN { get; set; }

        [Required(ErrorMessage = "Введите ОГРН")]
        [MaxLength (50, ErrorMessage = "Длина ОГРН не должна превышать 50 символов")]
        public string OGRN { get; set; }
        public List<Sellers> Sellers { get; set; }
    }
}
