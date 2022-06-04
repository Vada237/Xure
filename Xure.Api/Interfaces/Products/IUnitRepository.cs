using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IUnitRepository : IRepository<Units>
    {
        Units GetById(int id);

        Units GetByName(string name);
    }
}
