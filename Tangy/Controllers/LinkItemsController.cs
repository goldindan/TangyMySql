using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models;
using Tangy.Utility;

namespace Tangy.Controllers
{
    public class LinkItemsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LinkItemsController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.LinkItem.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST(LinkItem linkItem)
        {
            if (!ModelState.IsValid)
            {
                return View(linkItem);
            }

            _db.LinkItem.Add(linkItem);
            await _db.SaveChangesAsync();

            //Image Being saved
            String webRootPath = _hostingEnvironment.WebRootPath;

            var files = HttpContext.Request.Form.Files;

            var linkItemsfromDb = _db.LinkItem.Find(linkItem.Id);

            if (files[0] != null && files[0].Length > 0)
            {
                //When  user upload image
                var uploads = Path.Combine(webRootPath, "images-link");
                var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."));
                using (var filestream = new FileStream(Path.Combine(uploads, linkItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                linkItemsfromDb.Image = @"\images-link\" + linkItem.Id + extension;
            }
            else
            {
                //When user does not upload image.
                var uploads = Path.Combine(webRootPath, @"images-link\" + SD.DefaultLinkImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images-link\" + linkItem.Id + ".png");
                linkItemsfromDb.Image = @"\images-link\" + linkItem.Id + ".png";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET: Delete Menu Item
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LinkItem linkItem = await _db.LinkItem.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (linkItem == null)
            {
                return NotFound();
            }

            return View(linkItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            LinkItem linkItem = await _db.LinkItem.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (linkItem == null)
            {
                return NotFound();
            }

            var uploads = Path.Combine(webRootPath, "Images-link");

            string extension = string.Empty;

            if (linkItem.Image != null)
            {
                extension = linkItem.Image.Substring(linkItem.Image.LastIndexOf("."));
            }

            var imagePath = Path.Combine(uploads, linkItem.Id + extension);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _db.LinkItem.Remove(linkItem);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET: Edit Link Item
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LinkItem linkItem = await _db.LinkItem.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (linkItem == null)
            {
                return NotFound();
            }

            return View(linkItem);
        }

        //POST : Edit MenuItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LinkItem linkItem)
        {
            if (id != linkItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    var linkItemFromDb = await _db.LinkItem.Where(m => m.Id == id).FirstOrDefaultAsync();

                    if (files[0] != null && files[0].Length > 0)
                    {
                        //if user uploads a new image
                        var uploads = Path.Combine(webRootPath, "Images-link");
                        var extension_new = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."));
                        var extension_old = linkItemFromDb.Image.Substring(linkItemFromDb.Image.LastIndexOf("."));

                        if (System.IO.File.Exists(Path.Combine(uploads, linkItemFromDb.Id + extension_old)))
                        {
                            System.IO.File.Delete(Path.Combine(uploads, linkItemFromDb.Id + extension_old));
                        }

                        using (var filestream = new FileStream(Path.Combine(uploads, linkItemFromDb.Id + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }
                        linkItem.Image = @"\images-link\" + linkItemFromDb.Id + extension_new;
                    }

                    if (linkItem.Image != null)
                    {
                        linkItemFromDb.Image = linkItem.Image;
                    }

                    linkItemFromDb.Description = linkItem.Description;
                    linkItemFromDb.isActive = linkItem.isActive;
                    linkItemFromDb.OrderId = linkItem.OrderId;
                    linkItemFromDb.Subject = linkItem.Subject;
                    linkItemFromDb.Url = linkItem.Url;
                    await _db.SaveChangesAsync();
                }
                catch
                {

                }

                return RedirectToAction(nameof(Index));
            }
            return View(linkItem);
        }

        //GET: Details Link Item
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LinkItem linkItem = await _db.LinkItem.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (linkItem == null)
            {
                return NotFound();
            }

            return View(linkItem);
        }

    }
}