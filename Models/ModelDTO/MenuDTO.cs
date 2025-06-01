using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class MenuDTO
    {
        [Required]
        public string FoodName { get; set; }

        [Required]
        public float Price { get; set; }

        // Foreign key
        public int FoodCategoryId { get; set; }

    }
}
