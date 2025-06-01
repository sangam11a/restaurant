using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ItemId { get; set; }

        public StoreStatus Status { get; set; } = StoreStatus.Active;
    }

    public enum StoreStatus
    {
        Inactive = 0,
        Active = 1
    }
}
