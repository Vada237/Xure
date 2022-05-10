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
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Passport { get; set; }
        public bool Confirmed { get; set; }
        public string Avatar { get; set; }
        
        public Sellers Seller { get; set; }        
        public Clients Client { get; set; }
    }
}
