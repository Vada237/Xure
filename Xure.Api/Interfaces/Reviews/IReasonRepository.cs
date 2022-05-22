using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IReasonRepository : IRepository<Reason>
    {
        Reason Get(int id);
    }
}
