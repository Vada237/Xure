using Xure.Data;
using System.Linq;
using Xure.Api.Interfaces;


namespace Xure.Api.Services
{
    public class ReasonRepository  : Repository<Reason>,IReasonRepository
    {
        public ReasonRepository(AppDbContext context) : base(context)
        {

        }

        public Reason Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
