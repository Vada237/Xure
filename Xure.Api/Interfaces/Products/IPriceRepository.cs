using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IPriceRepository : IRepository<Prices>
    {
        Prices GetById(int id);
    }
}
