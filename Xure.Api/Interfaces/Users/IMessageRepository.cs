using Xure.Data;


namespace Xure.Api.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Message Get(int id);
    }
}
