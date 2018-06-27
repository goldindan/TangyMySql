using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Utility;

namespace Tangy.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class MedicalFieldsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MedicalFieldsController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET: Index
        public async Task<IActionResult> Index()
        {
            List<MedicalField> medicalFields = await _db.MedicalField.ToListAsync();
            return View(medicalFields);
        }

        //GET: Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalField medicalField)
        {
            if (ModelState.IsValid)
            {
                _db.Add(medicalField);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicalField);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalField = await _db.MedicalField.SingleOrDefaultAsync(m => m.Id == id);
            if (medicalField == null)
            {
                return NotFound();
            }
            return View(medicalField);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MedicalField medicalField)
        {
            if (id != medicalField.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(medicalField);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalFieldExists(medicalField.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicalField);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalField = await _db.MedicalField
                .SingleOrDefaultAsync(m => m.Id == id);
            if (medicalField == null)
            {
                return NotFound();
            }

            return View(medicalField);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalField = await _db.MedicalField
                .SingleOrDefaultAsync(m => m.Id == id);
            if (medicalField == null)
            {
                return NotFound();
            }

            return View(medicalField);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalField = await _db.MedicalField.SingleOrDefaultAsync(m => m.Id == id);
            _db.MedicalField.Remove(medicalField);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool MedicalFieldExists(int id)
        {
            return _db.MedicalField.Any(e => e.Id == id);
        }


    }
}