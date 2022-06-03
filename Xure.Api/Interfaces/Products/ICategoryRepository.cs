using Xure.Data;

namespace Xure.Api.Interfaces

{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetById(int id);
        Category GetByName(string name);
    }
}
