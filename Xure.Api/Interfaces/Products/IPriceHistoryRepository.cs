using Xure.Data;


namespace Xure.Api.Interfaces
{
    public interface IPriceHistoryRepository : IRepository<PriceHistory>
    {
       PriceHistory GetById(int id);
    }
}
