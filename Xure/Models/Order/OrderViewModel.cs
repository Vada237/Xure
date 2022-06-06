using Xure.Data;
using System.Collections.Generic;



namespace Xure.App.Models
{
    public class OrderViewModel
    {                
        public IEnumerable<ReceptionPoint> receptionPoints { get; set; }        
        public Order Order { get; set; }               
        public ReceptionPoint ReceptionPoint { get; set; }
     }
}
