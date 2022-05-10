using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        Delivery GetDelivery(int id);
    }
}
