using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Id4Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Id4Server.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginInput input)
        {
            return View(input);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}