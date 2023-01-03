using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Areas.Admin.Models
{
    public class CustomerEditViewModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Surname { get; set; }

        [Required]
        [StringLength(11)]
        public string TCKN { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(40)]
        public string? Email { get; set; }

        public bool IsBlacklist { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }
    }
}
