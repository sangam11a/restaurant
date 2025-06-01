using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Models
{
    public class FoodCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodCategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        
        //public ICollection<Menu> Menus { get; set; } = new List<Menu>();

    }
}
