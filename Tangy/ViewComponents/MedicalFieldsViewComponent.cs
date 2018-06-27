using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tangy.Data;

namespace Tangy.ViewComponents
{
    public class MedicalFieldsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public MedicalFieldsViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var medicalFieldsFromDb = await _db.MedicalField.OrderBy(t=>t.DisplayOrder).ToListAsync();

            return View(medicalFieldsFromDb);
        }


    }
}
