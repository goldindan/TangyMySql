using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Models.HomeViewModel;
using Tangy.Utility;

namespace Tangy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                MenuItems = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync(),
                Categories = _db.Category.OrderBy(c=>c.DisplayOrder).ToList(),
                Coupons = _db.Coupon.Where(c=>c.isActive).ToList()
            };
            return View(IndexVM);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var menuItemFromDB = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == id).FirstOrDefaultAsync();
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                MenuItem = menuItemFromDB,
                MenuItemId = id
            };

            return View(shoppingCart);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                shoppingCart.ApplicationUserId = claim.Value;

                ShoppingCart shoppingCartFromDB = await _db.ShoppingCart.Where(
                                                    s => s.ApplicationUserId == shoppingCart.ApplicationUserId &&
                                                    s.MenuItemId == shoppingCart.MenuItemId).FirstOrDefaultAsync();

                if(shoppingCartFromDB == null)
                {
                    //this menuitem  does no exist, add
                    _db.ShoppingCart.Add(shoppingCart);
                }
                else
                {
                    //this menuitem exist, update account
                    shoppingCartFromDB.Count += shoppingCart.Count;
                }

                await _db.SaveChangesAsync();

                var count = _db.ShoppingCart.Where(c => c.ApplicationUserId == shoppingCart.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32("CartCount", count);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var menuItemFromDB = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == shoppingCart.MenuItemId).FirstOrDefaultAsync();
                ShoppingCart newShoppingCart = new ShoppingCart()
                {
                    MenuItem = menuItemFromDB,
                    MenuItemId = shoppingCart.MenuItemId
                };

                return View(newShoppingCart);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
