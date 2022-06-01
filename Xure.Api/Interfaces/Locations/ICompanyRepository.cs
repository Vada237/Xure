using Xure.Data;
using System;

namespace Xure.Api.Interfaces
{    
    public interface ICompanyRepository : IRepository<Company>
    {
        public Company Get(int id);       
    }
}
