using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using RentACarSample.Common;
using RentACarSample.Entities;
using RentACarSample.Managers;
using RentACarSample.Models;

namespace RentACarSample.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private IMemberManager _memberManager;

        public AdminController(IMemberManager memberManager)
        {
            _memberManager = memberManager;
        }

        public IActionResult UserList()
        {
            List<Member> members = _memberManager.List();

            return View(members);
        }

        public IActionResult ResetPassword(int id)
        {
            string username = _memberManager.GetUsernameById(id);
            
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Username = username;
            
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(int id, ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                _memberManager.ResetPassword(model.Password, id);

                return RedirectToAction(nameof(UserList));
            }

            return View(model);
        }
    }
}
