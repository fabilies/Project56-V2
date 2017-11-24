using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project56_new.Data;
using Project56_new.Models;
using System.Security.Claims;

namespace Project56_new.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }
        public int CheckIfWishlistExist()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var WishMain = _context.WishMains.Where(w => w.user_ad == userId).FirstOrDefault();
            if (WishMain != null)
            {
                return WishMain.id;
            }
            else
            {
                return 0;
            }
        }
        // GET: Wishlist
        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<WishlistModel> model = null;
            int wishlist_id = CheckIfWishlistExist();

            if (wishlist_id != 0)
            {
                var WishLines = _context.WishLines.Where(l => l.Wishmain_id == wishlist_id).ToList();
                if (WishLines.Count() > 0)
                {
                    model = from wishlines in _context.WishLines
                            join itms in _context.Itms on wishlines.itm_id equals itms.id
                            join wishmain in _context.WishMains on wishlines.Wishmain_id equals wishmain.id
                            where wishlines.Wishmain_id == wishlist_id
                            select new WishlistModel
                            {
                                description = itms.description,
                                price = itms.price,
                                ordline_id = wishlines.id,
                                photo_url = itms.photo_url,
                                itm_id = itms.id
                            };
                    ViewBag.model_for_view = model;
                    return View(model.ToList());
                }
            }
            ViewBag.model_for_view = model;
            return View();
        }
        public async Task<ActionResult> addWishList(int itm_id)
        {
            int LRec_id;

            // get the logged-in user id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishmain = _context.WishMains.Where(w => w.user_ad == userId).FirstOrDefault();
            if (wishmain != null)
            {
                // record found 
                LRec_id = wishmain.id;
            }
            else
            {
                // no record yet
                WishMains wish = new WishMains();
                wish.user_ad = userId;
                wish.dt_created = DateTime.Now;
                _context.Add(wish);
                await _context.SaveChangesAsync();

                LRec_id = wish.id;
            }
           
                // Create new wishline
                WishLines wline = new WishLines();
                wline.itm_id = itm_id;
               wline.Wishmain_id = LRec_id;
                wline.dt_created = DateTime.Now;
                _context.Add(wline);
                await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));

        }

        public async Task<ActionResult> DeleteFromWishList(int ordline_id)
        {
            var result = (from wishlines in _context.WishLines
                          where wishlines.id == ordline_id
                          select wishlines).FirstOrDefault();
            if (result != null)
            {
                _context.WishLines.Remove(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.ErrorMessage = "Artikel kan niet verwijderd worden!";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
