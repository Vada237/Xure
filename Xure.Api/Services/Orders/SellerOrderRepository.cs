using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Xure.Api.Services
{
    public class SellerOrderRepository : Repository<SellerOrder>, ISellerOrderRepository
    {
        AppDbContext AppDbContext { get; set; }
        public SellerOrderRepository(AppDbContext context) : base(context)
        {
            AppDbContext = context;
        }

        public SellerOrder GetSellerOrder(int sellerOrderId)
        {
            return GetAll().FirstOrDefault(x => x.Id == sellerOrderId);
        }

        public SellerOrder GetWithOrderIdAndProductId(long orderId,long productId)
        {
            return GetWithInclude(c => c.OrderId == orderId, c => c.Delivery).FirstOrDefault();
        }
        public IEnumerable<SellerOrder> GetOrders()
        {
            var query = AppDbContext.SellerOrders.Include(x => x.Order).Include(x => x.Order.OrderProducts).ThenInclude(c => c.Product.Seller);                
            return query;
        }
    }
}
