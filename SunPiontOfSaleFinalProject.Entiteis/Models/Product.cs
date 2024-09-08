using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunPiontOfSaleFinalProject.Entiteis.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Price")]
        public double? DefaultPrice { get; set; }
        [ForeignKey(nameof(category))]
        public int categoryId { get; set; }
        public Category? category { get; set; }
        public string? ProductImage { get; set; }
    }
}
