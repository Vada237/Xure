using System.Linq;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Collections.Generic;
using System;

namespace Xure.Api.Services
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext context) : base(context)
        {

        }
        public Product GetById(long id)
        {
            return GetWithInclude(c => c.Id == id, c => c.Price.PriceHistory, c => c.Category, c => c.Brands, c=> c.ProductSpecificationsValues ).FirstOrDefault();
        }

        public void DeleteProduct(Product product)
        {
            Delete(product.Id);
        }
        public List<Product> FindProductsByName(string name)
        {
            return GetWithInclude(x => x.Name.Contains(name), x => x.Price.PriceHistory,x => x.Brands, x=> x.ProductSpecificationsValues, x=> x.Category).ToList();
        }

        public IEnumerable<Product> FindProductBySeller(string SellerId)
        {
            return GetWithInclude(c => c.Seller.UserId == SellerId, c => c.Seller, c => c.Brands, c => c.Category, c => c.Price.PriceHistory, c => c.Price.PriceHistory.Product);
        }

        public List<Product> FindProductByCategory(string CategoryName)
        {
            return GetWithInclude(c => c.Category.Name.Contains(CategoryName), c => c.Category, c => c.Price.PriceHistory).ToList();
        }
        public IEnumerable<Product> FindProducts(string? productName, string? categoryName, string? brandName,string? minPrice, string? maxPrice,string? productSpecifications)
        {
            IEnumerable<Product> products = GetWithInclude(c => c.Category,c => c.Price.PriceHistory, c => c.Brands, c => c.ProductSpecificationsValues);

            return products.Where(c => productName == null || c.Name.Contains(productName))
            .Where(c => categoryName == null || c.Category.Name.Contains(categoryName))
            .Where(C => brandName == null || C.Brands.Name.Contains(brandName))
            .Where(c => minPrice == null || c.Price.PriceHistory.Value > Convert.ToDecimal(minPrice))
            .Where(c => maxPrice == null || c.Price.PriceHistory.Value < Convert.ToDecimal(maxPrice));
        }
    }
}
