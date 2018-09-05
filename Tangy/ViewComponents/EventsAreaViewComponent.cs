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
    public class EventsAreaViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public EventsAreaViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var eventsDb = await _db.Event.Where(l=>l.IsActive).OrderBy(l=>l.OrderId).ToListAsync();

            return View(eventsDb);
        }

    }
}
