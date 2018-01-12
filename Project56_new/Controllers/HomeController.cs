using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project56_new.Models;
using Project56_new.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text;

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
        public async Task<IActionResult> Index(string search, string glass_type, string onsale, string maxPrice)
        {
            // Incoming information from the form.
            ViewData["CurrentFilter"] = search;
            ViewData["isOnSale"] = tf(onsale);
            ViewData["category"] = glass_type;
            ViewData["maxPrice"] = maxPrice;

            // Retrieve the products from the database.
            var products = from p in _context.Itms select p;
            products = products.Where(p => p.l_show.ToString() == "1");

            if (HttpContext.Request.Method == "POST")
            {
                if (ViewData["CurrentFilter"] != null)
                {
                    products = products.Where(p => p.description.Contains(ViewData["CurrentFilter"].ToString()));
                }

                if(ViewData["category"] != null)
                {
                    Debug.WriteLine($"CATEGORY: {ViewData["category"]}");
                   string cat = ViewData["category"].ToString();
                   if(cat == "Sterkte Brillen")
                   {
                        products = products.Where(p => p.category_id == 1);
                   }
                   else if(cat == "Zonne Brillen")
                   {
                        products = products.Where(p => p.category_id == 0);
                    }
                }

                if (ViewData["isOnSale"].ToString() == "1")
                {
                    products = products.Where(p => p.IsSales.ToString() == "1");
                }

                if(ViewData["maxPrice"] != null && float.Parse(ViewData["maxPrice"].ToString()) != 0f)
                {
                    products = products.Where(p => p.price <= float.Parse(ViewData["maxPrice"].ToString()));
                }

            }

            return View(await products.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactModel c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage sentMail = new MailMessage();
                    sentMail.IsBodyHtml = true;
                    // TODO Fix HTML weergave
                    sentMail.Body = " <div style='width:95%;'> " +
                                    " <img src='https://i.imgur.com/PqqYp7k.png' style='width: 75%; height: 135px;'/>" +
                                    " <table style='font-family: arial,sans-serif;border-collapse: collapse;width:75%; '>" +
                                    " <tr> " +
                                    " <th style='border: 1px solid #dddddd;text-align: left; padding:8px;font-size: 14pt;'>Naam:</th>" +
                                    " <th style='border: 1px solid #dddddd;text-align: left; padding:8px;font-size: 14pt;'>Van:</th>" +
                                    " <th style='border: 1px solid #dddddd;text-align: left; padding:8px;font-size: 14pt;'>Onderwerp:</th>" +
                                    " <th style='border: 1px solid #dddddd;text-align: left; padding:8px;font-size: 14pt;'>Bericht:</th>" +
                                    " </tr>" +
                                    " <tr>" +
                                    " <td style='border: 1px solid #dddddd;text-align: left; padding:8px;'>" + c.naam + "</td>" +
                                    " <td style='border: 1px solid #dddddd;text-align: left; padding:8px;'>" + c.email + "</td>" +
                                    " <td style='border: 1px solid #dddddd;text-align: left; padding:8px;'>" + c.onderwerp + "</td>" +
                                    " <td style='border: 1px solid #dddddd;text-align: left; padding:8px;'>" + c.bericht + "</td>" +
                                    "</table>" +
                                    "</div>";
                                    
                    sentMail.From = new MailAddress(c.email);
                    sentMail.To.Add("bdhstudiowebshop@gmail.com");
                    sentMail.Subject = c.onderwerp;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("bdhstudiowebshop@gmail.com", "Test@1234567!");

                    smtp.EnableSsl = true;
                    smtp.Send(sentMail);

                    ModelState.Clear();
                    ViewBag.Message = "Bedankt voor uw bericht, wij nemen zo spoedig mogelijk contact met u op! ";
                }
                catch (Exception err)
                {
                    ModelState.Clear();
                    ViewBag.Message = $"Er is een fout opgetreden {err.Message}";
                }
            }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
