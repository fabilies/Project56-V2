using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project56_new.Models;
using Project56_new.Data;
using Microsoft.EntityFrameworkCore;

namespace Project56_new.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public string tf(string _input)
        {
            if (_input == "on" || _input == "true")
                return "1";
            else
                return "-1";
        }

        // TODO : Modify it so it works out of the box. Might need to make a new
        // function as a whole out of it, but if it works here, hey atleast it works.
        public async Task<IActionResult> Index(string search, string onsale, string maxPrice)
        {
            // Incoming information from the form.
            ViewData["CurrentFilter"] = search;
            ViewData["isOnSale"] = tf(onsale);
            ViewData["maxPrice"] = maxPrice;

            // Retrieve the products from the database.
            var products = from p in _context.Itms select p;

            if (HttpContext.Request.Method == "POST")
            {
                Debug.WriteLine("Stuff works");
                //TODO : Create a querybuilder.
                if (ViewData["CurrentFilter"] != null)
                {
                    Debug.WriteLine("Ayy not null.");
                    products = products.Where(p => p.description.Contains(ViewData["CurrentFilter"].ToString()));
                }
                //TODO : Add Sale Check
                if (ViewData["isOnSale"].ToString() == "1")
                {
                    Debug.WriteLine("Ayy IS ON SALE BB");
                    products = products.Where(p => p.IsSales.ToString() == "1");
                }


                //TODO :  Price Check
                if(ViewData["maxPrice"] != null && float.Parse(ViewData["maxPrice"].ToString()) != 0f)
                {
                    Debug.WriteLine("MAX PRICE IS SET AYY");
                    products = products.Where(p => p.price <= float.Parse(ViewData["maxPrice"].ToString()));
                }

            }

            // Checking if the debug works out.
            Debug.Write("Houston, we have gotten the following info.");
            Debug.Write($"S : {ViewData["CurrentFilter"]} Sale:{ViewData["isOnSale"]} Max : {ViewData["maxPrice"]} ");


            return View(await products.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Favourites()
        {
            ViewData["Message"] = "Your Favourites page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
