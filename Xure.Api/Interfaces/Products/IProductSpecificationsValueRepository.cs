using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IProductSpecificationsValueRepository : IRepository<ProductSpecificationsValue>
    {
        public ProductSpecificationsValue getById(int id);
    }
}
