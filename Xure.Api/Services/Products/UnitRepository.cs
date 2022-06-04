using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Services
{
    public class UnitRepository : Repository<Units>, IUnitRepository
    {

        public UnitRepository(AppDbContext context) : base(context)
        {

        }

        public Units GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.id == id);
        }

        public Units GetByName(string name)
        {
            return GetAll().FirstOrDefault(c => c.Name == name);
        }
    }
}
