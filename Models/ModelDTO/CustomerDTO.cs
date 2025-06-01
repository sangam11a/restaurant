using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class CustomerDTO
    {
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

        //public TimeOnly TimeIn { get; set; }

    }
}
