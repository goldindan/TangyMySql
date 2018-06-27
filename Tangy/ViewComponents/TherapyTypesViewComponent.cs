using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tangy.Data;

namespace Tangy.ViewComponents
{
    public class TherapyTypesViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public TherapyTypesViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var therapyTypesFromDb = await _db.TherapyType.OrderBy(t=>t.DisplayOrder).ToListAsync();

            return View(therapyTypesFromDb);
        }


    }
}
