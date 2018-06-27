using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;

namespace Tangy.Controllers
{
    public class TherapistReviewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TherapistReviewController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET: Create
        public async Task<IActionResult> Create(int therapistId)
        {
            Therapist therapist = 
                await _db.Therapist.Include(t=>t.TherapyType).Where(t => t.Id == therapistId).FirstOrDefaultAsync();

            TherapistReview therapistReview = new TherapistReview();
            therapistReview.TherapistId = therapistId;
            therapistReview.Therapist = therapist;

            return View(therapistReview);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TherapistReview therapistReview)
        {

            if (!ModelState.IsValid)
            {
                return View(therapistReview);
            }

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            therapistReview.CreateUserId = claim.Value;
            therapistReview.CreateDate = DateTime.Now;

            therapistReview.Therapist =
                await _db.Therapist.Where(t => t.Id == therapistReview.TherapistId).FirstOrDefaultAsync();

            _db.TherapistReview.Add(therapistReview);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Therapist", new { TherapyTypeId = therapistReview.Therapist.TherapyTypeId });

        }
    }
}