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
            if (ModelState.IsValid)
            {
                Member member = _memberManager.Authenticate(model);

                if (member == null)
                {
                    ModelState.AddModelError("", "Hatalı kullanıcı adı ya da şifre!");
                }
                else
                {
                    // Cookie işlemi yapacağız.
                }
            }

            return View(model);
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
                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }
    }
}
