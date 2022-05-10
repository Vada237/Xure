using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IStorageRepository : IRepository<Storage>
    {
        Storage GetStorage(int id);
    }
}
