﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class MissingController : Controller
    {
        public IActionResult MissingDetail()
        {
            return View();
        }
    }
}