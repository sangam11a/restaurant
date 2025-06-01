using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public float Price { get; set; }

        // Foreign key
        public int FoodCategoryId { get; set; }

        // Navigation property
        public FoodCategory Category { get; set; }
    }

}
