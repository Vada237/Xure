using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IProductSpecificationsRepository : IRepository<ProductSpecifications>
    {
        public ProductSpecifications GetById(int id);
    }
}
