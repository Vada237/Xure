using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;

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

        [Required(ErrorMessage = "Укажите серию и номер пасспорта")]
        [UIHint("Серия и номер паспорта")]
        public string Passport { get; set; }
        public bool Confirmed { get; set; }

        [Required(ErrorMessage = "Добавьте аватар")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Поддерживаются только форматы: jpg, png, jpeg")]
        [UIHint("Аватар")]
        public IFormFile Avatar { get; set; }

        [UIHint("Почта")]
        [Required(ErrorMessage = "Укажите почту")]
        public string Email { get; set; }

        [UIHint("Пароль")]
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
    }

    class CreateClientViewModel
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

        [UIHint("Почта")]
        [Required(ErrorMessage = "Укажите почту")]
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
}
