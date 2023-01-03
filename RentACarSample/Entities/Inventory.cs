using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public int BranchId { get; set; }


        public Branch? Branch { get; set; }
    }
}
