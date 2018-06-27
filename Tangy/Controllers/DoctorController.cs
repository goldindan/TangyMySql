using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Models.DoctorViewModel;

namespace Tangy.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DoctorController(ApplicationDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //GET: Index
        public async Task<IActionResult> Index(int medicalFieldId)
        {
            var medicalFieldAsync = 
                _db.MedicalField.Where(tt => tt.Id == medicalFieldId).FirstOrDefaultAsync();

            var doctorsAsync = 
                _db.Doctor
                .Include(t=>t.DoctorReviews)
                .Where(t => t.MedicalFieldId == medicalFieldId).ToListAsync();

            DoctorViewModel doctorViewModel = new DoctorViewModel()
            {
                MedicalField = await medicalFieldAsync,
                Doctors = await doctorsAsync
            };

            return View(doctorViewModel);
        }

        //GET: Create
        public async Task<IActionResult> Create(int medicalFieldId)
        {

            Doctor doctor = new Doctor();

            doctor.MedicalField =
                await _db.MedicalField.Where(tt => tt.Id == medicalFieldId).FirstOrDefaultAsync();
            doctor.MedicalFieldId = medicalFieldId;

            return View(doctor);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            doctor.CreateUserId = claim.Value;
            doctor.CreateDate = DateTime.Now;

            _db.Doctor.Add(doctor);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { MedicalFieldId = doctor.MedicalFieldId });
        }

        //GET: Edit
        public async Task<IActionResult> Edit(int Id)
        {
            Doctor doctor = 
                await _db.Doctor.Include(t=>t.MedicalField).Where(t => t.Id == Id).FirstOrDefaultAsync();

            return View(doctor);
        }

        //POST:Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }

            Doctor doctorFromDb = 
                await _db.Doctor.Where(t => t.Id == doctor.Id).FirstOrDefaultAsync();

            doctorFromDb.Address = doctor.Address;
            doctorFromDb.City = doctor.City;
            doctorFromDb.Phone = doctor.Phone;
            doctorFromDb.Email = doctor.Email;
            doctorFromDb.Description = doctor.Description;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { MedicalFieldId = doctorFromDb.MedicalFieldId });
        }

        // GET: Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _db.Doctor.Include(t=>t.MedicalField)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _db.Doctor
                .SingleOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _db.Doctor.SingleOrDefaultAsync(m => m.Id == id);
            int medicalFieldId = doctor.MedicalFieldId;
            _db.Doctor.Remove(doctor);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { MedicalFieldId = medicalFieldId });
        }


    }
}