using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [StringLength(300)]
        public string Plate { get; set; }

        [Display(Name = "Daily Price")]
        [StringLength(300)]
        public decimal DailyPrice { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Display(Name = "Sub Brand")]
        public int SubBrandId { get; set; }

        public Brand Brand { get; set; }
        public SubBrand SubBrand { get; set; }

        public List<Rent> Rents { get; set; }
    }
}
