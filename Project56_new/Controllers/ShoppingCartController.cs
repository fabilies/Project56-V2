using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project56_new.Data;
using System.Security.Claims;
using Project56_new.Models;
using System.Net.Mail;
using System.Net;

namespace Project56_new.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
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
        public int CheckIfOrderExist()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            OrdMains OrdMain = _context.OrdMains.Where(o => o.user_ad == userId && o.ordstatus_id == 3).FirstOrDefault();
            if (OrdMain != null)
            {
                return OrdMain.id;
            }
            else
            {
                return 0;
            }
        }
        public int CreateOrder()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrdMains ord = new OrdMains();
            ord.user_ad = userId;
            ord.l_show = 1;
            ord.ordstatus_id = 3;
            ord.dt_created = DateTime.Now;
            _context.Add(ord);
            _context.SaveChanges();

            return ord.id;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IQueryable<ShoppingCartModel> model = null;
            // get the logged-in user id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _context.ApplicationUser.Where(u => u.Id == userId).FirstOrDefault();

            int order_id = CheckIfOrderExist();
            if (order_id != 0)
            {
                model = from ordlines in _context.OrdLines
                        join itms in _context.Itms on ordlines.itm_id equals itms.id
                        join ordmain in _context.OrdMains on ordlines.ord_id equals ordmain.id
                        where ordlines.ord_id == order_id
                        select new ShoppingCartModel
                        {
                            description = itms.description,
                            price = itms.price,
                            qty = ordlines.qty,
                            ordline_id = ordlines.id,
                            subtotal = ordlines.qty * itms.price,
                            photo_url = itms.photo_url,
                            stock = itms.itm_quantity,
                            ord_ad = order_id,
                            itm_id = itms.id

                        };
                ViewBag.model_for_view = model;
                ViewBag.email = result.Email;
                ViewBag.a_adres = result.a_adres;
                ViewBag.a_city = result.a_city;
                ViewBag.a_number = result.a_number;
                ViewBag.a_zipcode = result.a_zipcode;
                return View(model.ToList());
            }
            ViewBag.model_for_view = model;
            
            return View();

        }
        public async Task<ActionResult> SaveItmInShoppingCart(int itm_id)
        {
            int Lorder_id;
            int order_id = CheckIfOrderExist();
            if (order_id != 0)
            {
                // record found 
                Lorder_id = order_id;
            }
            else
            {
                Lorder_id = CreateOrder();
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

        public void DecreaseStock(int itm_id , int qty)
        {

            var item = (from i in _context.Itms
                        where i.id == itm_id
                        select i).FirstOrDefault();

            item.itm_quantity =  item.itm_quantity - qty;

            var result  = _context.Itms.Update(item);
        //    _context.SaveChanges();
       
        }
        [HttpPost]
        public IActionResult UpdateQtyInShoppingCart( int ordline_id , int qty)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _context.OrdMains.Where(o => o.user_ad == userId && o.ordstatus_id == 3).FirstOrDefault();

            var ListOfItems = _context.OrdLines.Where(o => o.ord_id == orders.id && o.id == ordline_id).FirstOrDefault(); ;

            ListOfItems.qty = qty;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
        public void CreateOrderHistoryRecord(OrdMains m )
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrdHistory h = new OrdHistory();
            var result = (from ordline in _context.OrdLines
                          join itm in _context.Itms on ordline.itm_id equals itm.id
                          select new OrdHistory
                          {
                              itm_description = itm.description,
                              ord_id = ordline.ord_id,
                              ordline_id = ordline.id,
                              priced_payed = itm.price,
                              qty_bought = ordline.qty,
                              dt_created = DateTime.Now,
                              user_ad = userId

                          }).ToList();

          foreach(var r in result)
            {

                _context.Add(r);
                _context.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder()
        {
            // get the logged-in user id
            Boolean decreased = false;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _context.ApplicationUser.Where(u => u.Id == userId).FirstOrDefault();

            var orders = _context.OrdMains.Where(o => o.user_ad == userId && o.ordstatus_id == 3).FirstOrDefault();
            var ListOfItems = _context.OrdLines.Where(o => o.ord_id == orders.id);
            if(decreased == false)
            {
                foreach (var l in ListOfItems)
                {
                    DecreaseStock(l.itm_id, l.qty);

                }
               
            }
            decreased = true;
            if ( decreased == true)
            {
                orders.ordstatus_id = 5;
                _context.Update(orders);
                _context.SaveChanges();
                CreateOrderHistoryRecord(orders);
            }          

            // send mail
            SmtpClient client = new SmtpClient("uxilo.com");
           // client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("webshop@uxilo.com", "webshop12345");
            client.Port = 587;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("no-reply@webshop.com");
            mailMessage.To.Add(result.Email);
            mailMessage.Body = "Bedankt voor het bestellen bij onze webshop.";
            mailMessage.Subject = "Order " + orders.id;
            client.Send(mailMessage);

            return RedirectToAction("index", "Home");
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<ShoppingCartModel> shoppingcart_model)
        {
            List<OrdLines> ordlines = new List<OrdLines>();
            
            foreach(var s in shoppingcart_model)
            {
                ordlines.Add(new OrdLines {  id = s.ordline_id, qty = s.qty , ord_id = s.ord_ad  , l_show = 1 , itm_id = s.itm_id});
            }

            foreach (var o in ordlines)
            {
                _context.OrdLines.Update(o);
                _context.SaveChanges();
                
            }
            return View(nameof(GetOrderView));
        }
        [HttpGet]
        public IActionResult GetOrderView()
        {
            return View();
        }
    }
}