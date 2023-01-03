using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public bool Hidden { get; set; }

        public List<SubBrand> SubBrands { get; set; }
        public List<Car> Cars { get; set; }
    }
}
