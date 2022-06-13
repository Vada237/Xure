using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Xure.Data;

namespace Xure.App.Models
{
    public class UserViewModel
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

        [Required(ErrorMessage = "Укажите серию и номер паспорта")]
        [UIHint("Серия и номер паспорта")]
        public string Passport { get; set; }
        public bool Confirmed { get; set; }
        
        [UIHint("Аватар")]
        public IFormFile Avatar { get; set; }

        [Required(ErrorMessage = "Укажите почту")]
        [UIHint("Почта")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "Добавьте пароль")]
        [UIHint("Пароль")]        
        public string Password { get; set; }
    }

    public class CreateClientViewModel
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
        
        [UIHint("Дата рождения")]
        public DateTime Birthday { get; set; }
        
        [UIHint("Серия и номер паспорта")]
        public string Passport { get; set; }
        public bool Confirmed { get; set; }

        [Required(ErrorMessage = "Укажите почту")]
        [UIHint("Почта")]        
        public string Email { get; set; }

        [UIHint("Пароль")]
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
    }

    public class CreateSellerViewModel
    {
        public UserViewModel User { get; set; }
        public CompanyViewModel Company { get; set; }
    }

    public class ProfileViewModel 
    { 
        public Clients? Client { get; set; }
        public Sellers? Seller { get; set; }
        public IEnumerable<SellerOrder>? SellerForOrders { get; set; }
        
        public int? CountSellerOrders { get; set; }
    }
}
