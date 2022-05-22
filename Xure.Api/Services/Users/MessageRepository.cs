using System.Linq;
using Xure.Data;
using Xure.Api.Interfaces;

namespace Xure.Api.Services
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {

        }

        public Message Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
