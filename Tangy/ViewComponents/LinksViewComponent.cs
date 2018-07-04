using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tangy.Data;

namespace Tangy.ViewComponents
{
    public class LinksViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public LinksViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var linksDb = await _db.LinkItem.ToListAsync();

            return View(linksDb);
        }

    }
}
