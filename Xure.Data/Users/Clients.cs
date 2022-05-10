using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; 
namespace Xure.Data
{
    public class Clients
    {
        public int Id { get; set; }
        public AppUser UserInfo { get; set; }
        public string UserId { get; set; }

        public List<Reviews> Reviews { get; set; }
        public List<Message> ClientMessages { get; set; }
        public List<Order> Orders { get; set; }
    }
}
