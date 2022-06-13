using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Xure.Api.Services
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public AppDbContext Context { get; set; }

        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public Order GetOrder(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public Order GetOrder(long id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void SaveOrder(Order order)
        {            
            Context.AttachRange(order.CartLines.Select(c => c.Product));
            if (order.Id == 0)
            {
                Create(order);
            }
        }
    }
}
