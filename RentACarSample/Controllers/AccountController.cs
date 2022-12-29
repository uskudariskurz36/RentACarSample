﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RentACarSample.Entities;
using RentACarSample.Managers;
using RentACarSample.Models;
using System.Linq.Expressions;
using System.Security.Claims;

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
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("memberid", member.Id.ToString()));
                    claims.Add(new Claim("username", member.Username));

                    foreach (MemberRole role in member.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
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

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
