using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;

namespace Xure.Api.Services
{
    public class ProductSpecificationsRepository : Repository<ProductSpecifications>, IProductSpecificationsRepository
    {

        public ProductSpecificationsRepository(AppDbContext appDbContext) : base(appDbContext) { }        
        public ProductSpecifications GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
