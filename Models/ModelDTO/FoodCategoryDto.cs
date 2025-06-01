using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class FoodCategoryDto
    {
        [Required(ErrorMessage = "Food category Name is missing")]
        public string CategoryName { get; set; }
    }
}
