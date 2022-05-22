using System.Linq;
using Xure.Data;
using Xure.Api.Interfaces;


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
    }

}
