using Microsoft.AspNetCore.Http;
using Xure.Data;

namespace Xure.App.Models
{
    public class EditProfileViewModel
    {
        public AppUser User { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
