using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class MemberRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; }
    }
}
