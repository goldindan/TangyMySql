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
    public class TherapyTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TherapyTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET: Index
        public async Task<IActionResult> Index()
        {
            List<TherapyType> therapyTypes = await _db.TherapyType.ToListAsync();
            return View(therapyTypes);
        }

        //GET: Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TherapyType therapyType)
        {
            if (ModelState.IsValid)
            {
                _db.Add(therapyType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(therapyType);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapyType = await _db.TherapyType.SingleOrDefaultAsync(m => m.Id == id);
            if (therapyType == null)
            {
                return NotFound();
            }
            return View(therapyType);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TherapyType therapyType)
        {
            if (id != therapyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(therapyType);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TherapyTypeExists(therapyType.Id))
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
            return View(therapyType);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapyType = await _db.TherapyType
                .SingleOrDefaultAsync(m => m.Id == id);
            if (therapyType == null)
            {
                return NotFound();
            }

            return View(therapyType);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapyType = await _db.TherapyType
                .SingleOrDefaultAsync(m => m.Id == id);
            if (therapyType == null)
            {
                return NotFound();
            }

            return View(therapyType);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var therapyType = await _db.TherapyType.SingleOrDefaultAsync(m => m.Id == id);
            _db.TherapyType.Remove(therapyType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool TherapyTypeExists(int id)
        {
            return _db.TherapyType.Any(e => e.Id == id);
        }


    }
}