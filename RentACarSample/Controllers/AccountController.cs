using Microsoft.AspNetCore.Mvc;
using RentACarSample.Entities;
using RentACarSample.Managers;
using RentACarSample.Models;
using System.Linq.Expressions;

namespace RentACarSample.Controllers
{
    public class AccountController : Controller
    {
        private IMemberManager _memberManager;

        public AccountController(IMemberManager memberManager)
        {
            _memberManager = memberManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _memberManager.AddMember(model);
            }

            return View(model);
        }
    }
}
