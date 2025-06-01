using System.ComponentModel.DataAnnotations;

namespace Stock.Models.ModelDTO
{
    public class StoreDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ItemId { get; set; }

        public StoreStatus Status { get; set; } = StoreStatus.Active;

    }
}
