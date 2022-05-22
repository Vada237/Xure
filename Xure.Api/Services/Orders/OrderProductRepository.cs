using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Xure.Api.Services
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(AppDbContext context) : base(context)
        {

        }

        public OrderProduct GetByIds(long orderId, long productId)
        {
            return GetAll().FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
        }

        public IEnumerable<OrderProduct> GetByOrderId(long id)
        {
            return GetAll().Where(x => x.OrderId == id);
        }

        public IEnumerable<OrderProduct> GetByProductId(long id)
        {
            return GetAll().Where(x => x.ProductId == id);
        }
        
        public string GetProductName(long id)
        {
            return GetAll().FirstOrDefault(c => c.ProductId == id).Product.Name;
        }
    }
}
