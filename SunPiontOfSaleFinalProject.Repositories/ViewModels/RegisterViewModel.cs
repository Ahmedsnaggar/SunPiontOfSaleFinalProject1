using System.ComponentModel.DataAnnotations;

namespace SunPiontOfSaleFinalProject.Repositories.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="User name is required")]
        [StringLength(256)]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Email not valid")]
        [StringLength(256)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password not match")]
        public string ConfirmPassword { get; set; }
    }
}
