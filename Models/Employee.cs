using Stock.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Key]
    public Guid EmpId { get; set; }

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
    public string Phone { get; set; } = string.Empty;

    // Navigation property to related Bills
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
