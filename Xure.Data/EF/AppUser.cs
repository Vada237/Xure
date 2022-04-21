using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Xure.Data
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Passport { get; set; }
        public bool Confirmed { get; set; }
        public string Avatar { get; set; }
        public Company Company { get; set; }
        public long CompanyId { get; set; }
        public List<Order> Orders { get; set; }
        public List<Message> ClientMessages { get; set; }
        public List<Message> SellerMessages { get; set; }
        public List<Product> Products { get; set; }
        public List<Reviews> Reviews { get; set; }
    }
}
