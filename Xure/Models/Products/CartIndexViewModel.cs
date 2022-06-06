using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }       
        
        public List<CartLine> cartLines { get; set; }
    }
}
