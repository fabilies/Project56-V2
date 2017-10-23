using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project56_new.Data;
using Project56_new.Models;

namespace Project56_new.Controllers
{
    public class ProductDetails : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductDetails(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductDetails/View/5
        public async Task<IActionResult> View(int? id)
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
    }
}
