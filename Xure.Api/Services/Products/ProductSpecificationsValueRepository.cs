using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Services
{
    public class ProductSpecificationsValueRepository : Repository<ProductSpecificationsValue>, IProductSpecificationsValueRepository
    {
        public ProductSpecificationsValueRepository(AppDbContext context) : base(context)
        {

        }
        public ProductSpecificationsValue getById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
