using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IClientRepository : IRepository<Clients>
    {
        public Clients Get(int id);                       
    }
}
