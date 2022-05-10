using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Services
{
    public class DeliveryRepository : Repository<Delivery>, IDeliveryRepository
    {

        public DeliveryRepository(AppDbContext context) : base(context)
        {

        }

        public Delivery GetDelivery(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }
    }
}
