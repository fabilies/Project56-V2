using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project56_new.Data;
using Project56_new.Models;
namespace Project56_new.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public StatisticsController (ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var total_orders = _context.OrdMains.Count();
            var amount_products = _context.Itms.Count();
            double total_money_exclusive = _context.OrdHistory.Sum(x => (x.priced_payed * x.qty_bought) );
            int total_money_exc = (int)total_money_exclusive;
            double total_money_inclusive = ((total_money_exc / 100 * 21)  + total_money_exc);

            


            ViewBag.total_orders = total_orders;
            ViewBag.amount_products = amount_products;
            ViewBag.total_money_exclusive = total_money_exclusive;
            ViewBag.total_money_inclusive = total_money_inclusive;
            return View();
        }
    }
}