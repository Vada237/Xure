using Xure.Data;
using System.Collections.Generic;

namespace Xure.Api.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product GetById(long id);
        public List<Product> FindProductsByName(string name);
        
        public void DeleteProduct(Product product)
        {
            Delete(product.Id);
        }
        public IEnumerable<Product> FindProductBySeller(string SellerId);

        public List<Product> FindProductByCategory(string CategoryName);
        public List<Product> FindProducts(string productName, string categoryName, string brandName, string minPrice, string maxPrice, string productSpecifications);
    }
}
