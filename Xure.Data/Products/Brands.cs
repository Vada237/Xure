using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Xure.Data
{
    public class Brands
    {
        public int  Id { get; set; }

        [Required(ErrorMessage = "Введите название бренда")]
        [MaxLength(100,ErrorMessage = "Название бренда не должно превышать 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите страну-производитель")]
        [MaxLength(30,ErrorMessage = "Длина страны не должна превышать 30 символов")]
        public string Country { get; set; }
        [IgnoreDataMember]
        public List<Product> Products { get; set; }
    }
}
