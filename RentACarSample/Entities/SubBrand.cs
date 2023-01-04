using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class SubBrand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int BrandId { get; set; }

        public Brand? Brand { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
