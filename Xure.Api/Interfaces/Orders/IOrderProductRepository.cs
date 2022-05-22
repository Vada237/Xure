using Xure.Data;
using System.Collections.Generic;

namespace Xure.Api.Interfaces
{
    public interface IOrderProductRepository : IRepository<OrderProduct>
    {
        IEnumerable<OrderProduct> GetByProductId(long id);
        IEnumerable<OrderProduct> GetByOrderId(long id);
        OrderProduct GetByIds(long orderId, long productId);                
        string GetProductName(long productId);
        void Delete(long id);
    }
}
