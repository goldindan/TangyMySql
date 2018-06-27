using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Models.UserViewModel;
using Tangy.Utility;

namespace Tangy.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager
            )
        {
            _db = db;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            UserViewModel userViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(await _db.Users.Where(u=>u.Email!=user.Email).Select(u => new UserViewModel()
                                                                        {
                                                                            Id = u.Id,
                                                                            FirstName = u.FirstName,
                                                                            LastName = u.LastName,
                                                                            Email = u.Email,
                                                                            PhoneNumber = u.PhoneNumber
                                                                        }).ToListAsync());
        }

        //GET : Edit
        public async Task<IActionResult> Edit(String Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            var user = await _db.Users.Where(u => u.Id.Equals(Id)).FirstOrDefaultAsync();

            if(user == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                LockoutEnd = (user.LockoutEnd.HasValue?user.LockoutEnd.Value.DateTime:(DateTime?)null),
                LockoutReason = user.LockoutReason,
                PhoneNumber = user.PhoneNumber
            };

            return View(userViewModel);
        }

        //POST : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //Get user from db
                var user = await _db.Users.Where(u => u.Id == userViewModel.Id).FirstOrDefaultAsync();
                if(user == null)
                {
                    return NotFound();
                }

                //Update user from db
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.PhoneNumber = userViewModel.PhoneNumber;
                if (userViewModel.LockoutEnd.HasValue)
                {
                    user.LockoutEnd = DateTime.SpecifyKind(userViewModel.LockoutEnd.Value, DateTimeKind.Utc);
                }
                else
                {
                    user.LockoutEnd = null;
                }
                user.LockoutReason = userViewModel.LockoutReason;
                user.AccessFailedCount = userViewModel.AccessFailedCount;

                //Save user changes
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        //GET : Lock
        public async Task<IActionResult> Lock(String Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            var user = await _db.Users.Where(u => u.Id.Equals(Id)).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                LockoutEnd = (user.LockoutEnd.HasValue ? user.LockoutEnd.Value.DateTime : (DateTime?)null),
                LockoutReason = user.LockoutReason,
                PhoneNumber = user.PhoneNumber
            };

            return View(userViewModel);
        }

        //POST : Lock
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Lock(UserViewModel userViewModel)
        {
            //Get user from db
            var user = await _db.Users.Where(u => u.Id == userViewModel.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutReason = userViewModel.LockoutReason;
            user.LockoutEnd = DateTime.SpecifyKind(DateTime.Now.Date.AddYears(100), DateTimeKind.Utc);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}