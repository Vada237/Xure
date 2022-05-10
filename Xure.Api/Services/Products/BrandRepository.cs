using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;


namespace Xure.Api.Services
{
    public class BrandRepository : Repository<Brands>, IBrandRepository

    {
        public BrandRepository(AppDbContext context) : base(context)
        {

        }
        public Brands getById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);    
        }
    }
}
