using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AppUser Client { get; set; }
        public string ClientId { get; set; }
        public AppUser Seller { get; set; }
        public string SellerId { get; set; }

    }
}
