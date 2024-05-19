using Microsoft.AspNetCore.Identity;

namespace Boulderlog.Data.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser() { }

        public AppUser(string username)
        {
            UserName = username;
        }
    }
}
