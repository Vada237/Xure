using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Xure.Data
{
    public class Message
    {
        public int Id { get; set; }

        [MinLength(1,ErrorMessage = "Введите сообщение")]
        [MaxLength(1000,ErrorMessage = "Длина сообщения не должна превышать 1000 символов")]
        public string Text { get; set; }
        
        
        public AppUser Sender { get; set; }        
        public string SenderId { get; set; }
        public AppUser Recipient { get; set; }

        public string RecipientId { get; set; }
        public DateTime MessageTime { get; set; }

        public byte[] Image { get; set; }
    }
}
