using Microsoft.AspNetCore.Identity;

namespace boulderlog.Data.Models
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
