﻿using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
