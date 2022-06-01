using Xure.Data;
using System.Collections.Generic;

namespace Xure.Api.Interfaces {
    public interface ISellerRepository : IRepository<Sellers> { 
        public Company FindCompanyByEmail(string email);
        public Company FindCompanyByPhone(string phone);
        public Company FindCompanyByNameAndSurname(string Name,string Surname);

        public IEnumerable<Sellers> GetSellers();
        public IEnumerable<Sellers> GetSellersWithInclude();        
    }
}
