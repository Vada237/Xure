using Xure.Data;

namespace Xure.Api.Interfaces {
    public interface ISellerRepository { 
        public Company FindCompanyByEmail(string email);
        public Company FindCompanyByPhone(string phone);
        public Company FindCompanyByNameAndSurname(string Name,string Surname);
    }
}
