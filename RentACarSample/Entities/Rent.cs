using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Entities
{
    public class Rent
    {
        [Key]
        public Guid Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int? KM { get; set; }
        public decimal Price { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool Received { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public Car Car { get; set; }
        public Customer Customer { get; set; }
    }
}
