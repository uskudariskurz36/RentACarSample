using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Models
{
    public class ResetPasswordViewModel
    {
        public string? Username { get; set; }

        [Required]
        [MinLength(6), MaxLength(16)]
        public string Password { get; set; }
    }
}
