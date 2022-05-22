using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;

namespace Xure.Api.Services
{
    public class ReviewsRepository : Repository<Reviews>, IReviewsRepository
    {
        public ReviewsRepository(AppDbContext context) : base(context)
        {

        }

        public Reviews Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
