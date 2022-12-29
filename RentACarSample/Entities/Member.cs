using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }

        public List<MemberRole> Roles { get; set; }
    }
}
