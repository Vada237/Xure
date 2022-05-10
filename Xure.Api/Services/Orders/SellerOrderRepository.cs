using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;


namespace Xure.Api.Services
{
    public class SellerOrderRepository : Repository<SellerOrder>, ISellerOrderRepository
    {
        public SellerOrderRepository(AppDbContext context) : base(context)
        {

        }

        public SellerOrder GetSellerOrder(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
