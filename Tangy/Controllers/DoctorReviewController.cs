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
    public class DoctorReviewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DoctorReviewController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET: Create
        public async Task<IActionResult> Create(int doctorId)
        {
            Doctor doctor = 
                await _db.Doctor.Include(t=>t.MedicalField).Where(t => t.Id == doctorId).FirstOrDefaultAsync();

            DoctorReview doctorReview = new DoctorReview();
            doctorReview.DoctorId = doctorId;
            doctorReview.Doctor = doctor;

            return View(doctorReview);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorReview doctorReview)
        {

            if (!ModelState.IsValid)
            {
                return View(doctorReview);
            }

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            doctorReview.CreateUserId = claim.Value;
            doctorReview.CreateDate = DateTime.Now;

            doctorReview.Doctor =
                await _db.Doctor.Where(t => t.Id == doctorReview.DoctorId).FirstOrDefaultAsync();

            _db.DoctorReview.Add(doctorReview);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Doctor", new { MedicalFieldId = doctorReview.Doctor.MedicalFieldId });

        }
    }
}