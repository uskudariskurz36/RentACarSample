using Microsoft.AspNetCore.Mvc;

namespace RentACarSample.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
