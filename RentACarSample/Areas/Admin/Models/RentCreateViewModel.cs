using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Areas.Admin.Models
{
    public class RentCreateViewModel
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int? KM { get; set; }
        public decimal Price { get; set; }
        public DateTime ReceivedDate { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }

        public SelectList? Cars { get; set; }
        public SelectList? Customers { get; set; }
    }
}
