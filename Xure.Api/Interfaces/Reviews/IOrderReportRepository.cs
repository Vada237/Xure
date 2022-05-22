using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IOrderReportRepository : IRepository<OrderReport>
    {
        public OrderReport Get(int id);
    }
}
