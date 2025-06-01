using Stock.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrdId { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }

    public bool Status { get; set; } = false;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

