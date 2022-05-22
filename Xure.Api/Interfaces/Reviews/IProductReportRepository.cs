using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IProductReportRepository : IRepository<ProductReport>
    {
        ProductReport Get(int id);
    }
}
