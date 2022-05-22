using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IReviewsRepository : IRepository<Reviews>
    {
        public Reviews Get(int id);
       
    }
}
