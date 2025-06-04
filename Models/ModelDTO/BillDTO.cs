using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class BillDTO
    {
        public long Amount { get; set; }

        public bool Status { get; set; } = false;

        public DateTime PaymentTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please enter employee id")]
        public Guid EmpId { get; set; }  // FK to Employee

        [ValidateNever]
        public Employee Employee { get; set; }  // Navigation to Employee

        [Required(ErrorMessage = "Please enter customer id")]
        public Guid CustomerId { get; set; }  // FK to Customer

        [ValidateNever]
        public Customer Customer { get; set; }  // Navigation to Customer

    }
}
