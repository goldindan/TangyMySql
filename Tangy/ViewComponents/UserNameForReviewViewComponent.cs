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
    public class UserNameForReview : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public UserNameForReview(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            //var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userFromDb = await _db.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            return View(userFromDb);
        }

    }
}
