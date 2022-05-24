using Microsoft.AspNetCore.Identity;
using Xure.Data;
using System.Collections.Generic;
using System.Linq;

namespace Xure.Api.Services
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext AppDbContext { get; set; }        
        public UserRepository(AppDbContext context)
        {
            AppDbContext = context;
        }

        public IEnumerable<IdentityUser> GetUsers()
        {     
            if (AppDbContext.Users == null)
            {
                return null;
            }
            return AppDbContext.Users;
        }
    }
}
