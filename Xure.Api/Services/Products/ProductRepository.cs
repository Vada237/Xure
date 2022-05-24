using System.Linq;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Collections.Generic;

namespace Xure.Api.Services
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext context) : base(context)
        {

        }
        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public List<Product> FindProductsByName(string name)
        {
            return GetAll().Where(x => x.Name.Contains(name)).ToList();
        }
    }

}
