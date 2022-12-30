using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarSample.Common;
using RentACarSample.Entities;
using RentACarSample.Managers;

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

            return View();
        }
    }
}
