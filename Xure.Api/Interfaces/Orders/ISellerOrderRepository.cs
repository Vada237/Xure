using Xure.Data;
using System.Collections.Generic;

namespace Xure.Api.Interfaces
{
    public interface ISellerOrderRepository : IRepository<SellerOrder>
    {
        SellerOrder GetSellerOrder(int id);

        IEnumerable<SellerOrder> GetOrders();
    }
}
