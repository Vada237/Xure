using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IReceptionPointRepository : IRepository<ReceptionPoint>
    {
        ReceptionPoint GetById (int id);
    }
}
