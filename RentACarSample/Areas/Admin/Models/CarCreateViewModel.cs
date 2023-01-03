using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RentACarSample.Areas.Admin.Models
{
    public class CarCreateViewModel
    {
        [StringLength(300)]
        public string Plate { get; set; }

        public decimal DailyPrice { get; set; }

        public bool IsAvailable { get; set; }

        public int BrandId { get; set; }
        public int SubBrandId { get; set; }

        public SelectList? Brands { get; set; }
    }
}
