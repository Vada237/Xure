using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateRegistration { get; set; }
        public string INN { get; set; }
        public string OGRN { get; set; }
        public List<Sellers> Sellers { get; set; }
    }
}
