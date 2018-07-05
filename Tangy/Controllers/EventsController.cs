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
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EventsController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Event.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            _db.Event.Add(@event);
            await _db.SaveChangesAsync();

            //Image Being saved
            String webRootPath = _hostingEnvironment.WebRootPath;

            var files = HttpContext.Request.Form.Files;

            var eventsfromDb = _db.Event.Find(@event.Id);

            if (files[0] != null && files[0].Length > 0)
            {
                //When  user upload image
                var uploads = Path.Combine(webRootPath, "images-event");
                var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."));
                using (var filestream = new FileStream(Path.Combine(uploads, @event.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                eventsfromDb.Image = @"\images-event\" + @event.Id + extension;
            }
            else
            {
                //When user does not upload image.
                var uploads = Path.Combine(webRootPath, @"images-event\" + SD.DefaultEventImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images-event\" + @event.Id + ".png");
                eventsfromDb.Image = @"\images-event\" + @event.Id + ".png";
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

            Event @event = await _db.Event.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Event @event = await _db.Event.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (@event == null)
            {
                return NotFound();
            }

            var uploads = Path.Combine(webRootPath, "Images-event");

            string extension = string.Empty;

            if (@event.Image != null)
            {
                extension = @event.Image.Substring(@event.Image.LastIndexOf("."));
            }

            var imagePath = Path.Combine(uploads, @event.Id + extension);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _db.Event.Remove(@event);

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

            Event @event = await _db.Event.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        //POST : Edit MenuItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    var eventFromDb = await _db.Event.Where(m => m.Id == id).FirstOrDefaultAsync();

                    if (files[0] != null && files[0].Length > 0)
                    {
                        //if user uploads a new image
                        var uploads = Path.Combine(webRootPath, "Images-event");
                        var extension_new = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."));
                        var extension_old = eventFromDb.Image.Substring(eventFromDb.Image.LastIndexOf("."));

                        if (System.IO.File.Exists(Path.Combine(uploads, eventFromDb.Id + extension_old)))
                        {
                            System.IO.File.Delete(Path.Combine(uploads, eventFromDb.Id + extension_old));
                        }

                        using (var filestream = new FileStream(Path.Combine(uploads, eventFromDb.Id + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }
                        @event.Image = @"\images-event\" + eventFromDb.Id + extension_new;
                    }

                    if (@event.Image != null)
                    {
                        eventFromDb.Image = @event.Image;
                    }

                    eventFromDb.Description = @event.Description;
                    eventFromDb.IsActive = @event.IsActive;
                    eventFromDb.OrderId = @event.OrderId;
                    eventFromDb.Name = @event.Name;
                    eventFromDb.EndDate = @event.EndDate;
                    eventFromDb.StartDate = @event.StartDate;
                    await _db.SaveChangesAsync();
                }
                catch
                {

                }

                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        //GET: Details Link Item
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event @event = await _db.Event.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

    }
}