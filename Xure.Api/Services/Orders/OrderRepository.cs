using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;

namespace Xure.Api.Services
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {

        }
        public Order GetOrder(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
