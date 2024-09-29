using System.ComponentModel.DataAnnotations;

namespace SunPiontOfSaleFinalProject.Repositories.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
