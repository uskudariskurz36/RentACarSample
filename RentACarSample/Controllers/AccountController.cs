using Microsoft.AspNetCore.Mvc;
using RentACarSample.Models;

namespace RentACarSample.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }
    }
}
