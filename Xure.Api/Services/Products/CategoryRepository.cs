using Xure.Api.Interfaces;
using Xure.Data;
using System.Linq;


namespace Xure.Api.Services
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public Category GetById(int id)
        {            
            return GetAll().FirstOrDefault(x => x.Id == id);                        
        }
        
        public Category GetByName(string name)
        {
            return GetAll().Where(x => x.Name == name).FirstOrDefault();
        }        
    }
}
