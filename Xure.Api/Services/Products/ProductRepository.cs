using System.Linq;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Collections.Generic;

namespace Xure.Api.Services
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext context) : base(context)
        {

        }
        public Product GetById(int id)
        {
            return GetWithInclude(c => c.Id == id, c => c.Price.PriceHistory, c => c.Category, c => c.Brands, c=> c.ProductSpecificationsValues ).FirstOrDefault(x => x.Id == id);
        }

        public List<Product> FindProductsByName(string name)
        {
            return GetWithInclude(x => x.Name.Contains(name), x => x.Price.PriceHistory).ToList();
        }

        public IEnumerable<Product> FindProductBySeller(string SellerId)
        {
            return GetWithInclude(c => c.Seller.UserId == SellerId, c => c.Seller, c => c.Brands, c => c.Category, c => c.Price.PriceHistory, c => c.Price.PriceHistory.Product);
        }

        public List<Product> FindProductByCategory(string CategoryName)
        {
            return GetWithInclude(c => c.Category.Name.Contains(CategoryName), c => c.Category, c => c.Price.PriceHistory).ToList();
        }        
    }
}
