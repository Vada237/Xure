using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class AppUser : IdentityUser
    {       
        
        [Required]
        public string Name { get; set; }

        [Required]        
        public string Surname { get; set; }

        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Passport { get; set; }
        public bool Confirmed { get; set; }
        public byte[] Avatar { get; set; }
        
        public Sellers Seller { get; set; }        
        public Clients Client { get; set; }
        public List<Message> senderMessages { get; set; }
        public List<Message> recipientMessages { get; set; }
    }
}
