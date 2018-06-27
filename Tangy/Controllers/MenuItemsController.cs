using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Models.MenuItemViewModels;
using Tangy.Utility;

namespace Tangy.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        [BindProperty]
        public MenuItemViewModel MenuItemVM { get; set; }

        public MenuItemsController(ApplicationDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _db = dbContext;
            _hostingEnvironment = hostingEnvironment;
            MenuItemVM = new MenuItemViewModel()
            {
                Categories = _db.Category.ToList(),
                MenuItem = new Models.MenuItem()
            };
        }

        //GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var menuItems = _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory);
            return View(await menuItems.ToListAsync());
        }

        //GET: Create
        public IActionResult Create()
        {
            return View(MenuItemVM);
        }


        //POST: Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            MenuItemVM.MenuItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());
            if(!ModelState.IsValid)
            {
                return View(MenuItemVM);
            }

            _db.MenuItem.Add(MenuItemVM.MenuItem);
            await _db.SaveChangesAsync();

            //Image Being saved
            String webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var menuItemfromDb = _db.MenuItem.Find(MenuItemVM.MenuItem.Id);

            if(files[0] != null && files[0].Length > 0)
            {
                //When  user upload image
                var uploads = Path.Combine(webRootPath, "images");
                var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."));
                using (var filestream = new FileStream(Path.Combine(uploads, MenuItemVM.MenuItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                menuItemfromDb.Image = @"\images\" + MenuItemVM.MenuItem.Id + extension;
            }
            else
            {
                //When user does not upload image.
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultFoodImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + MenuItemVM.MenuItem.Id + ".png");
                menuItemfromDb.Image = @"\images\" + MenuItemVM.MenuItem.Id + ".png";

            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }



        public JsonResult GetSubCategory(int CategoryId)
        {
            List<SubCategory> subCategorieList = new List<SubCategory>();
            subCategorieList = (from subCategory in _db.SubCategory
                                where subCategory.CategoryId == CategoryId
                                select subCategory).ToList();

            return Json(new SelectList(subCategorieList, "Id", "Name"));
        }

        //GET: Edit Menu Item
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MenuItemVM.MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == id).FirstOrDefaultAsync();
            MenuItemVM.SubCategories = _db.SubCategory.Where(s => s.CategoryId == MenuItemVM.MenuItem.CategoryId).ToList();

            if(MenuItemVM.MenuItem == null)
            {
                return NotFound();
            }

            return View(MenuItemVM);
        }

        //POST : Edit MenuItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            MenuItemVM.MenuItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (id!=MenuItemVM.MenuItem.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    var menuItemFromDb = await _db.MenuItem.Where(m => m.Id == id).FirstOrDefaultAsync();

                    if(files[0] != null && files[0].Length > 0)
                    {
                        //if user uploads a new image
                        var uploads = Path.Combine(webRootPath, "Images");
                        var extension_new = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."));
                        var extension_old = menuItemFromDb.Image.Substring(menuItemFromDb.Image.LastIndexOf("."));

                        if (System.IO.File.Exists(Path.Combine(uploads, menuItemFromDb.Id + extension_old)))
                        {
                            System.IO.File.Delete(Path.Combine(uploads, menuItemFromDb.Id + extension_old));
                        }

                        using (var filestream = new FileStream(Path.Combine(uploads, menuItemFromDb.Id + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }
                        MenuItemVM.MenuItem.Image = @"\images\" + menuItemFromDb.Id + extension_new;
                    }

                    if(MenuItemVM.MenuItem.Image != null)
                    {
                        menuItemFromDb.Image = MenuItemVM.MenuItem.Image;
                    }

                    menuItemFromDb.Name = MenuItemVM.MenuItem.Name;
                    menuItemFromDb.Description = MenuItemVM.MenuItem.Description;
                    menuItemFromDb.Price = MenuItemVM.MenuItem.Price;
                    menuItemFromDb.Spicyness = MenuItemVM.MenuItem.Spicyness;
                    menuItemFromDb.CategoryId = MenuItemVM.MenuItem.CategoryId;
                    menuItemFromDb.SubCategoryId = MenuItemVM.MenuItem.SubCategoryId;
                    await _db.SaveChangesAsync();
                }
                catch
                {

                }

                return RedirectToAction(nameof(Index));
            }
            MenuItemVM.SubCategories = _db.SubCategory.Where(s => s.CategoryId == MenuItemVM.MenuItem.CategoryId).ToList();
            return View(MenuItemVM);
        }

        //GET: Details Menu Item
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MenuItemVM.MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == id).FirstOrDefaultAsync();

            if (MenuItemVM.MenuItem == null)
            {
                return NotFound();
            }

            return View(MenuItemVM);
        }

        //GET: Delete Menu Item
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MenuItemVM.MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == id).FirstOrDefaultAsync();

            if (MenuItemVM.MenuItem == null)
            {
                return NotFound();
            }

            return View(MenuItemVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            MenuItem menuItem = await _db.MenuItem.Where(m => m.Id == id).FirstOrDefaultAsync();

            if(menuItem == null)
            {
                return NotFound();
            }

            var uploads = Path.Combine(webRootPath, "Images");

            string extension = string.Empty;

            if (menuItem.Image != null)
            {
                extension = menuItem.Image.Substring(menuItem.Image.LastIndexOf("."));
            }

            var imagePath = Path.Combine(uploads, menuItem.Id + extension);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _db.MenuItem.Remove(menuItem);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}