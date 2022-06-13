using Xure.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Xure.App.Models{
    public class ReportInfoViewModel
    {
        public OrderReport OrderReport { get; set; }
        public IEnumerable<Message> Messages { get; set; }        
        public AppUser Sender { get; set; }
        public Message Message { get; set; }

        public IFormFile Img { get; set; }
        
    }
}
