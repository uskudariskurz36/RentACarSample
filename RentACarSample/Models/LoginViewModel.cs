using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }
    }
}
