using Microsoft.AspNetCore.Identity;
using SunPiontOfSaleFinalProject.Entiteis.Models;

namespace SunPiontOfSaleFinalProject.Repositories.Context
{
    public class AppUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
