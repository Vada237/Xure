using System.Linq;
using Xure.Api.Interfaces;
using Xure.Data;

namespace Xure.Api.Services
{
    public class OrderReportRepository : Repository<OrderReport>, IOrderReportRepository
    {
        public OrderReportRepository(AppDbContext context) : base(context)
        {

        }

        public OrderReport Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
