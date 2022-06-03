using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;


namespace Xure.Api.Services
{
    public class PriceHistoryRepository : Repository<PriceHistory>, IPriceHistoryRepository
    {
        public PriceHistoryRepository(AppDbContext appDbContext) : base(appDbContext) 
        {

        }

        public PriceHistory GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }        
    }
}
