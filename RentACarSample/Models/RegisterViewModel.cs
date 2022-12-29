using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [MinLength(6), MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [MinLength(6), MaxLength(16)]
        public string RePassword { get; set; }
    }
}
