using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Services
{
    public class ReceptionPointRepository : Repository<ReceptionPoint>, IReceptionPointRepository
    {
        public ReceptionPointRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public ReceptionPoint GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.id == id);
        }
    }
}
