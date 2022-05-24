using Xure.Data;
using System.Collections.Generic;

namespace Xure.Api.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product GetById(int id);
        public List<Product> FindProductsByName(string name);


    }
}
