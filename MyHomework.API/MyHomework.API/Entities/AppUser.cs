using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyHomework.API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
            