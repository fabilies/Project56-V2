using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project56_new.Data;
using System.Security.Claims;
using Project56_new.Models;

namespace Project56_new.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
         // 1 : klikken op verwijderen
         // 2 : form method naar deze method sturen met parameter ordline_id {razor}

        [HttpPost]
        public ActionResult DeleteItmFromShoppingCart(string ordline_id)
        {
             
            return Ok();
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var OrdMain = _context.OrdMains.Where(o => o.user_ad == userId && o.ordstatus_id == 3).FirstOrDefault();

            // var OrdLines = _context.OrdLines.Where(line => line.ord_id == OrdMain.id).ToList();
            var ShoppingCartItems = from ordlines in _context.OrdLines
                                    join itms in _context.Itms on ordlines.itm_id equals itms.id

                                    select new ShoppingCartModel 
                                    {
                                        description = itms.description,
                                        price = itms.price,
                                        qty = ordlines.qty,
                                        ordline_id = ordlines.id,
                                        subtotal = ordlines.qty *itms.price,
                                        photo_url = itms.photo_url
                                     };


            if (ShoppingCartItems == null)
            {
                return View();
            }
            else {
               
                var TotalPice =  ShoppingCartItems.Sum(s => s.subtotal);
                ViewBag.Total = TotalPice;
                return View(ShoppingCartItems.ToList());
            }  
        }
        public async Task<ActionResult> SaveItmInShoppingCart(int itm_id)
        {
            int Lorder_id;

            // get the logged-in user id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = _context.OrdMains.Where(o => o.user_ad == userId && o.ordstatus_id == 3).FirstOrDefault();
            if (orders != null)
            {
                // record found 
                Lorder_id = orders.id;
            }
            else
            {
                // no record yet
                OrdMains ord = new OrdMains();
                ord.user_ad = userId;
                ord.l_show = 1;
                ord.ordstatus_id = 3;
                ord.dt_created = DateTime.Now;
                _context.Add(ord);
                await _context.SaveChangesAsync();

                Lorder_id = ord.id;
            }
            

            var CheckItmId = _context.OrdLines.Where(i => i.itm_id == itm_id).FirstOrDefault();
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
                OrdLines oline = new OrdLines();
                oline.itm_id = itm_id;
                oline.l_show = 1;
                oline.ord_id = Lorder_id;
                oline.dt_created = DateTime.Now;
                oline.qty = 1;
                _context.Add(oline);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}