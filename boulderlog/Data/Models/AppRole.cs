using Microsoft.AspNetCore.Identity;

namespace Boulderlog.Data.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() { }

        public AppRole(string role)
        {
            this.Name = role;
            this.NormalizedName = role.ToLower();
        }
    }
}
