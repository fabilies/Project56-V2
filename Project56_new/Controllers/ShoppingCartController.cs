﻿using System;
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

        [HttpGet]
        // deze functie
        public async Task<ActionResult> DeleteItmFromShoppingCart(int ordline_id)
        {
            var result = (from ordlines in _context.OrdLines
                          where ordlines.id == ordline_id
                          select ordlines).FirstOrDefault();
            if (result != null)
            {
                _context.OrdLines.Remove(result);
               await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.ErrorMessage = "Artikel kan niet verwijderd worden!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

           var OrdMain = _context.OrdMains.Where(o => o.user_ad == userId && o.ordstatus_id == 3).FirstOrDefault();
           var OrdLines = _context.OrdLines.Where(line => line.ord_id == OrdMain.id).ToList();
           if (OrdLines.Count() > 0)
           {
                var ShoppingCartItems = from ordlines in _context.OrdLines
                                        join itms in _context.Itms on ordlines.itm_id equals itms.id
                                        join ordmain in _context.OrdMains on ordlines.ord_id equals ordmain.id
                                        where ordlines.ord_id == OrdMain.id
                                        select new ShoppingCartModel
                                        {
                                            description = itms.description,
                                            price = itms.price,
                                            qty = ordlines.qty,
                                            ordline_id = ordlines.id,
                                            subtotal = ordlines.qty * itms.price,
                                            photo_url = itms.photo_url
                                        };
                return View(ShoppingCartItems.ToList());
            }
            return View();
            

                
            
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
            

            var CheckItmId = _context.OrdLines.Where(i => i.itm_id == itm_id && i.ord_id == Lorder_id).FirstOrDefault();
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

        [HttpPost]
        public  ActionResult ConfirmOrder()
        {
            // get the logged-in user id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = _context.OrdMains.Where(o => o.user_ad == userId && o.ordstatus_id == 3).FirstOrDefault();
            orders.ordstatus_id = 5;
            _context.Update(orders);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

      //  [HttpGet]
        public  IActionResult GetOrderView() {
            return View();
        }


    }

}