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
        public Clients Client { get; set; }
        public int ClientId { get; set; }
        public Sellers Seller { get; set; }
        public int SellerId { get; set; }
        public DateTime MessageTime { get; set; }

        public string Image { get; set; }
    }
}
