using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class EmployeeDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string Shift { get; set; } = string.Empty;

        public float Salary { get; set; }

        public DateOnly DOB { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MinLength(6, ErrorMessage = "Phone number cannot be empty.")]
        public string Phone { get; set; } 

       
    }
}
