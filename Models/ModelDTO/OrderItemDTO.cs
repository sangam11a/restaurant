using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class OrderItemDTO
    {
        [Required]
        public int MenuId { get; set; }
        [Required(ErrorMessage ="Please enter MenuID")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Please enter the Price")]

        public float Price { get; set; }

        public int OrderId { get; set; }
    }
}
