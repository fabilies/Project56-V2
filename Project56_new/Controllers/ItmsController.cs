using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project56_new.Data;
using Project56_new.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

namespace Project56_new.Controllers
{
    public class ItmsController : Controller
    {
        private readonly ApplicationDbContext _context;

       
        public ItmsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public  IActionResult ShoppingCarat()
        {
            return View();
        }
        // GET: Itms
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Itms.ToListAsync());
        }

        // GET: Itms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itms = await _context.Itms
                .SingleOrDefaultAsync(m => m.id == id);
            if (itms == null)
            {
                return NotFound();
            }

            return View(itms);
        }

        // GET: Itms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Itms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,description,long_description,category_id,price,photo_url,l_show,dt_created,dt_modified")] Itms itms )
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {

                        var file = Image;
                        var uploads = Path.Combine("wwwroot\\images\\products\\");

                        if (file.Length > 0)
                        {
                            Random rnd = new Random();
                            int num = rnd.Next(0000000, 9999999);
                            
                            var fileName = ContentDispositionHeaderValue.Parse
                                (file.ContentDisposition).FileName.Trim('"');


                            string Key = num + fileName;
                            
                            System.Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, Key), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                itms.photo_url = Key;

                            }


                        }
                    }
                }
                _context.Add(itms);
                await _context.SaveChangesAsync();
           
                return RedirectToAction(nameof(Index));

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(itms);
        }

 

        // GET: Itms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itms = await _context.Itms.SingleOrDefaultAsync(m => m.id == id);
            if (itms == null)
            {
                return NotFound();
            }
            return View(itms);
        }

        // POST: Itms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,description,long_description,category_id,price,photo_url, itm_quantity,l_show,dt_created,dt_modified")] Itms itms)
        {
            if (id != itms.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItmsExists(itms.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itms);
        }

        // GET: Itms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itms = await _context.Itms
                .SingleOrDefaultAsync(m => m.id == id);
            if (itms == null)
            {
                return NotFound();
            }

            return View(itms);
        }

        // POST: Itms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itms = await _context.Itms.SingleOrDefaultAsync(m => m.id == id);
            _context.Itms.Remove(itms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItmsExists(int id)
        {
            return _context.Itms.Any(e => e.id == id);
        }
    }
}
