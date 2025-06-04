using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public int Type { get; set; }

        public int TableNumber { get; set; }

        public TimeOnly TimeIn { get; set; }

        public TimeOnly TimeOut { get; set; }

        public int BillId { get; set; }
        // Navigation
        [ValidateNever]
        public ICollection<Bill> Bills{ get; set; } = new List<Bill>();

        [ValidateNever]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
