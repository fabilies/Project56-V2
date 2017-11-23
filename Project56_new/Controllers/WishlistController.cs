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

        // GET: Wishlist
        [HttpGet]
        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var WishMain = _context.WishMains.Where(w => w.user_ad == userId && w.ordstatus_id == 3).FirstOrDefault();
            var WishLines = _context.WishLines.Where(l => l.ord_id == WishMain.id).ToList();
            if (WishLines.Count() > 0)
            {
                var WishlistItems = from wishlines in _context.WishLines
                                        join itms in _context.Itms on wishlines.itm_id equals itms.id
                                        join wishmain in _context.WishMains on wishlines.ord_id equals wishmain.id
                                        where wishlines.ord_id == WishMain.id
                                        select new WishlistModel
                                        {
                                            description = itms.description,
                                            price = itms.price,
                                            qty = wishlines.qty,
                                            ordline_id = wishlines.id,
                                            subtotal = wishlines.qty * itms.price,
                                            photo_url = itms.photo_url
                                        };
                return View(WishlistItems.ToList());
            }
            return View();




        }
        public async Task<ActionResult> addWishList(int itm_id)
        {
            int Lorder_id;

            // get the logged-in user id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = _context.WishMains.Where(w => w.user_ad == userId && w.ordstatus_id == 3).FirstOrDefault();
            if (orders != null)
            {
                // record found 
                Lorder_id = orders.id;
            }
            else
            {
                // no record yet
                WishMains ord = new WishMains();
                ord.user_ad = userId;
                ord.l_show = 1;
                ord.ordstatus_id = 3;
                ord.dt_created = DateTime.Now;
                _context.Add(ord);
                await _context.SaveChangesAsync();

                Lorder_id = ord.id;
            }


            var CheckItmId = _context.WishLines.Where(i => i.itm_id == itm_id && i.ord_id == Lorder_id).FirstOrDefault();
            // item exist
            if (CheckItmId != null)
            {
                CheckItmId.qty = CheckItmId.qty + 1;
                _context.Update(CheckItmId);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Create new orderline
                WishLines oline = new WishLines();
                oline.itm_id = itm_id;
                oline.l_show = 1;
                oline.ord_id = Lorder_id;
                oline.dt_created = DateTime.Now;
                oline.qty = 1;
                _context.Add(oline);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
            //return View(await _context.WishlistModel.ToListAsync());
        }


        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlistModel = await _context.WishlistModel
                .SingleOrDefaultAsync(m => m.id == id);
            if (wishlistModel == null)
            {
                return NotFound();
            }

            return View(wishlistModel);
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
