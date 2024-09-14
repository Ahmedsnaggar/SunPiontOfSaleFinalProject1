using System.ComponentModel.DataAnnotations;

namespace SunPiontOfSaleFinalProject.Entiteis.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name can not by empty")]
        public string CategoryName { get; set; }
        [MaxLength(150)]
        [Display(Name = "Description")]
        public string? CategoryDescription { get; set; }

        
    }
}
