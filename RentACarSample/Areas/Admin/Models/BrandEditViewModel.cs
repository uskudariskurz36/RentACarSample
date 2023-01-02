using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Areas.Admin.Models
{
    public class BrandEditViewModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public bool Hidden { get; set; }
    }
}
