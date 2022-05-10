using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface ISellerOrderRepository : IRepository<SellerOrder>
    {
        SellerOrder GetSellerOrder(int id);
    }
}
