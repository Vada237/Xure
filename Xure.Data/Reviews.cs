using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Reviews
    {
        public long Id { get; set; }

        public AppUser Client;
        public string ClientId { get; set; }
        public Product Product {get;set;}
        public long ProductId { get; set; }
        public byte Rating { get; set; }
        public string Commentary { get; set; }
    }
}
