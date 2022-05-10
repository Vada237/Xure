using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Xure.Data
{
    public class Sellers 
    {        
        public int Id { get; set; }
        public AppUser UserInfo { get; set; }
        public string UserId { get; set; }
        public Company Company { get; set; }
        public long CompanyId { get; set; }        
        public List<Product> Products { get; set; }
        public List<Message> SellerMessages { get; set; }

    }
}
