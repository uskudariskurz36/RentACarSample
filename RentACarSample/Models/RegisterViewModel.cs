using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [StringLength(150)]
        public string RePassword { get; set; }
    }
}
