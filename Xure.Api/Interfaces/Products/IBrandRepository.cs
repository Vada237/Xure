using System.Linq;
using Xure.Data;


namespace Xure.Api.Interfaces
{
    public interface IBrandRepository : IRepository<Brands>
    {
        Brands getById(int id);

        Brands GetByName(string name);
    }
}
