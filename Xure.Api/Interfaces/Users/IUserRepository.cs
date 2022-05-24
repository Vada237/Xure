using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Xure.Api.Services
{
    public interface IUserRepository
    {
        IEnumerable<IdentityUser> GetUsers();        
    }
}
