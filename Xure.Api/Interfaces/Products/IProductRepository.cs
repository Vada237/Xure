using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product GetById(int id);
    }
}
