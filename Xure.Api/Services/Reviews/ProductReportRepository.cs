using System.Linq;
using Xure.Data;
using Xure.Api.Interfaces;


namespace Xure.Api.Services
{
    public class ProductReportRepository : Repository<ProductReport>, IProductReportRepository
    {
        public ProductReportRepository(AppDbContext context) : base(context)
        {

        }

        public ProductReport Get(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }
    }
}
