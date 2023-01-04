using Microsoft.AspNetCore.Mvc;

namespace RentACarSample.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
