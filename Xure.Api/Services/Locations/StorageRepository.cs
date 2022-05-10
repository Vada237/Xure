using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Services
{
    public class StorageRepository : Repository<Storage>, IStorageRepository
    {
        public StorageRepository(AppDbContext context) : base(context)
        {

        }

        public Storage GetStorage(int id)
        {
            return GetAll().FirstOrDefault(c => c.id == id);
        }
    }
}
