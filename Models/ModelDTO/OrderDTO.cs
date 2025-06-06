using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class OrderDTO
    {
        //public int? OrdId { get; set; } // Nullable for POST, required for PUT
        public Guid CustomerId { get; set; }

        public bool Status { get; set; } = false;

        //[Required(ErrorMessage = "Please enter the dish id")]
        //public int DishId { get; set; }

        //[Required(ErrorMessage = "Please enter the price")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        //public float Price { get; set; }

        //[Required(ErrorMessage = "Please enter the customer id")]
        //public Guid CustomerId { get; set; }

        //public bool Status { get; set; } = false;
    }
}
