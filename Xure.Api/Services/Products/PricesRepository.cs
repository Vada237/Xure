using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Services
{
    public class PricesRepository : Repository<Prices>, IPriceRepository
    {
        public PricesRepository(AppDbContext context) : base(context)
        {

        }

        public Prices GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
