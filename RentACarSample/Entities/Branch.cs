using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(30)]
        public string? Phone { get; set; }

        public List<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
