using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Models.TherapistViewModel;

namespace Tangy.Controllers
{
    public class TherapistController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TherapistController(ApplicationDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //GET: Index
        public async Task<IActionResult> Index(int therapyTypeId)
        {
            var therapyTypeAsync = 
                _db.TherapyType.Where(tt => tt.Id == therapyTypeId).FirstOrDefaultAsync();

            var therapistsAsync = 
                _db.Therapist
                .Include(t=>t.TherapistReviews)
                .Where(t => t.TherapyTypeId == therapyTypeId).ToListAsync();

            TherapistViewModel therapistViewModel = new TherapistViewModel()
            {
                TherapyType = await therapyTypeAsync,
                Therapists = await therapistsAsync
            };

            return View(therapistViewModel);
        }

        //GET: Create
        public async Task<IActionResult> Create(int therapyTypeId)
        {

            Therapist therapist = new Therapist();

            therapist.TherapyType =
                await _db.TherapyType.Where(tt => tt.Id == therapyTypeId).FirstOrDefaultAsync();
            therapist.TherapyTypeId = therapyTypeId;

            return View(therapist);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Therapist therapist)
        {
            if (!ModelState.IsValid)
            {
                return View(therapist);
            }

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            therapist.CreateUserId = claim.Value;
            therapist.CreateDate = DateTime.Now;

            _db.Therapist.Add(therapist);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { TherapyTypeId = therapist.TherapyTypeId });
        }

        //GET: Edit
        public async Task<IActionResult> Edit(int Id)
        {
            Therapist therapist = 
                await _db.Therapist.Include(t=>t.TherapyType).Where(t => t.Id == Id).FirstOrDefaultAsync();

            return View(therapist);
        }

        //POST:Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Therapist therapist)
        {
            if (!ModelState.IsValid)
            {
                return View(therapist);
            }

            Therapist therapistFromDb = 
                await _db.Therapist.Where(t => t.Id == therapist.Id).FirstOrDefaultAsync();

            therapistFromDb.Address = therapist.Address;
            therapistFromDb.City = therapist.City;
            therapistFromDb.Phone = therapist.Phone;
            therapistFromDb.Email = therapist.Email;
            therapistFromDb.Description = therapist.Description;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { TherapyTypeId = therapistFromDb.TherapyTypeId });
        }

        // GET: Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _db.Therapist.Include(t=>t.TherapyType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (therapist == null)
            {
                return NotFound();
            }

            return View(therapist);
        }

        // GET: Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _db.Therapist
                .SingleOrDefaultAsync(m => m.Id == id);
            if (therapist == null)
            {
                return NotFound();
            }

            return View(therapist);
        }

        // POST: Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var therapist = await _db.Therapist.SingleOrDefaultAsync(m => m.Id == id);
            int therapistTypeId = therapist.TherapyTypeId;
            _db.Therapist.Remove(therapist);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { TherapyTypeId = therapistTypeId });
        }


    }
}