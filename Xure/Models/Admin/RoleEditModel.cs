using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using Xure.Data;

namespace Xure.App.Models
{

    public class RoleViewModel {

        public IEnumerable<AppUser> users;

        public IEnumerable<IdentityRole> roles;
    }

    public class RoleEditModel
    {
    public IdentityRole Role { get; set; }
    public IEnumerable<AppUser> Members { get; set; }
    public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {

        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }    
}
