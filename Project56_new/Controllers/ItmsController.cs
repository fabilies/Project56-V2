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
using Microsoft.AspNetCore.Hosting;

namespace Project56_new.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItmsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        private readonly ApplicationDbContext _context;


        public ItmsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            hostingEnvironment = environment;

        }
            // GET: Itms
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
        public  void ItmImage(Itms itms)
        {

            var files = HttpContext.Request.Form.Files;
            
            foreach (var Image in files)
            {
                if (Image.FileName != "") {
                var uniqueFileName = GetUniqueName(Image.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "images/products");
                var filePath = Path.Combine(uploads, uniqueFileName);
                 Image.CopyTo(new FileStream(filePath, FileMode.Create));
                itms.photo_url = uniqueFileName;
                }
            }
               
        }
        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
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
        public async Task<IActionResult> Create([Bind("id,description,long_description,category_id,IsSales,price,photo_url,l_show,dt_created,dt_modified , itm_quantity")] Itms itms )
        {
            if (ModelState.IsValid)
            {

                this.ItmImage(itms);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,description,long_description,category_id,IsSales,price,photo_url,l_show,dt_created,dt_modified , itm_quantity")] Itms itms)
        {
            if (id != itms.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                 
                      this.ItmImage(itms);
                      if (itms.photo_url == "" || itms.photo_url == null)
                    {
                        var result = (from i in _context.Itms
                                      where i.id == id
                                      select i.photo_url).FirstOrDefault() ;

                        itms.photo_url = result;

                    }
                    
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

        //// GET: Itms/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var itms = await _context.Itms
        //        .SingleOrDefaultAsync(m => m.id == id);
        //    if (itms == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(itms);
        //}

        // GET: Itms/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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
