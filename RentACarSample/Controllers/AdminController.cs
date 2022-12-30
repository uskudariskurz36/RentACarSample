﻿using Microsoft.AspNetCore.Authorization;
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
        private IMemberRoleManager _memberRoleManager;

        public AdminController(IMemberManager memberManager, IMemberRoleManager memberRoleManager)
        {
            _memberManager = memberManager;
            _memberRoleManager = memberRoleManager;
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

        
        public IActionResult ChangeRoles(int id)
        {
            string username = _memberManager.GetUsernameById(id);

            ChangeRolesViewModel model = new ChangeRolesViewModel();
            model.Username = username;

            List<MemberRole> roles = _memberRoleManager.GetRolesByMemberId(id);

            model.IsAdmin = roles.Any(x => x.Name == Roles.Admin);
            model.IsUser = roles.Any(x => x.Name == Roles.User);

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeRoles(int id, ChangeRolesViewModel model, [FromServices]IServiceProvider serviceProvider)
        {
            DatabaseContext _databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();   

            if (model.IsAdmin)
            {
                
            }

            return View();
        }
    }
}
