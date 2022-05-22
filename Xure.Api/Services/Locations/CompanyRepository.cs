using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;

namespace Xure.Api.Services
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {

        }

        public Company Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
