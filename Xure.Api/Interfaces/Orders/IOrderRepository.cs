using Xure.Data;


namespace Xure.Api.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrder(int id);
    }
}
