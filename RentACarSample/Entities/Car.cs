using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [StringLength(300)]
        public string Plate { get; set; }

        [StringLength(300)]
        public decimal DailyPrice { get; set; }

        public bool IsAvailable { get; set; }

        public int BrandId { get; set; }
        public int SubBrandId { get; set; }

        public Brand Brand { get; set; }
        public SubBrand SubBrand { get; set; }
    }
}
