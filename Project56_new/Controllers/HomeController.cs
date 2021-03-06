﻿using System;
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
using System.Net;

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
                                    "<br><h2> Wij hebben het volgende van uw ontvangen en onze klantenservice zal spoedig contact met u opnemen</h2><br>" +
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
                                    "</table><br>" +
                                    "Het is ook mogelijk om ons te bereiken via ons telefoonnummer : 0182 - 999999 op Maandag t/m Vrijdag tussen 9:00 en 17:00."+
                                    "</div>";
                                    
                    sentMail.From = new MailAddress("webshop@uxilo.com");
                    sentMail.To.Add(c.email);
                    sentMail.Subject = c.onderwerp;

                    SmtpClient client = new SmtpClient();
                    client.Host = "uxilo.com";
                    client.Port = 587;
                    // client.UseDefaultCredentials = false;
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential("webshop@uxilo.com", "webshop123");

 
                    client.Send(sentMail);

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
