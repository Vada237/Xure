using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Xure.Data;

namespace Xure.Api.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<IdentityUser> GetUsers();        
    }
}
