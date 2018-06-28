using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tangy.Controllers
{
    public class RightsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(string rightId)
        {
            return View(nameof(Show),rightId);
        }
    }
}