using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tangy.Data;

namespace Tangy.Controllers
{
    public class LinkItemsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LinkItemsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.LinkItem.ToList());
        }
    }
}