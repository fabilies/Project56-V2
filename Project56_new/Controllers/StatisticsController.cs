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
        public ActionResult Index(int? page)
        {
            var total_orders = _context.OrdMains.Count();
            var amount_products = _context.Itms.Count();
            var total_money = _context.OrdHistory.Sum(x => (x.priced_payed * x.qty_bought) );

            ViewBag.total_orders = total_orders;
            ViewBag.amount_products = amount_products;
            ViewBag.total_money = total_money;
            return View();
        }
    }
}