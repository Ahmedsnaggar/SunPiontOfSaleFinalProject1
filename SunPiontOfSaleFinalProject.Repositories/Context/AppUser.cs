using Microsoft.AspNetCore.Identity;

namespace SunPiontOfSaleFinalProject.Repositories.Context
{
    public class AppUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
