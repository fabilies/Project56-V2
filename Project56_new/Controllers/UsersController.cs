using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project56_new.Data;
using Project56_new.Models;
using Microsoft.AspNetCore.Authorization;
using Project56_new.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Project56_new.Services;
using Microsoft.Extensions.Logging;

namespace Project56_new.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.UsersDb.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.UsersDb
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Users/Create
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // User account aanmaken via beheer asah
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email,
                                                 Email = model.Email,
                                                 firstname = model.Firstname,
                                                 middlename = model.Middlename,
                                                 lastname = model.Lastname,
                                                 dt_birth = model.Dt_birth,
                                                 gender = model.Gender,
                                                 a_zipcode = model.Zipcode,
                                                 a_adres = model.Adress,
                                                 a_city = model.City,
                                                 a_number = model.Homenumber,
                                                 PhoneNumber = model.Phonenumber};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    return RedirectToLocal("/Users");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var applicationUser = await _context.UsersDb.SingleOrDefaultAsync(
            //    m => m.Id == id
            //    );

            var applicationUser = (from a in _context.ApplicationUser
                                  where a.Id == id
                                  select new RegisterViewModel
                                  {

                                      UserName = a.UserName,
                                      Email = a.Email,
                                      Firstname = a.firstname,
                                      Middlename = a.middlename,
                                      Lastname = a.lastname,
                                      Dt_birth = a.dt_birth,
                                      Gender = a.gender,
                                      Zipcode = a.a_zipcode,
                                      Adress = a.a_adres,
                                      City = a.a_city,
                                      Homenumber = a.a_number,
                                      Phonenumber = a.PhoneNumber
                                  }).FirstOrDefault();

            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Bind("PasswordHash,a_zipcode,a_adres,a_city,a_number,firstname,middlename,lastname,dt_birth,gender,Id,UserName,Email,PhoneNumber")]
        public async Task<IActionResult> Edit(string id, [Bind("UserName, Password, Firstname, Middlename, Lastname, Dt_birth, Gender, Zipcode, Adress, City, Homenumber, Email, Phonenumber")] RegisterViewModel applicationUser)
        {
            var search_id = (from u in _context.ApplicationUser
                             where u.Id == id
                             select u).FirstOrDefault();


            search_id.Id = id;
            search_id.firstname = applicationUser.Firstname;
            search_id.lastname = applicationUser.Lastname;
            search_id.middlename = applicationUser.Middlename;
            search_id.PhoneNumber = applicationUser.Phonenumber;
            search_id.UserName = search_id.UserName;
            search_id.a_adres = applicationUser.Adress;
            search_id.a_city = applicationUser.City;
            search_id.a_number = applicationUser.Homenumber;
            search_id.a_zipcode = applicationUser.Zipcode;
            search_id.Email = applicationUser.Email;
            search_id.gender = applicationUser.Gender;
            search_id.PasswordHash = search_id.PasswordHash;


            if (search_id.Id == null)
            {
                return NotFound();
            }

            else
            {
                _context.Update(search_id);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var applicationUser = await _context.UsersDb
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (applicationUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(applicationUser);
        //}

        // POST: Users/Delete/5
        [HttpGet]   
        public async Task<IActionResult> Delete(string id)
        {
            var applicationUser = await _context.UsersDb.SingleOrDefaultAsync(m => m.Id == id);
            _context.UsersDb.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.UsersDb.Any(e => e.Id == id);
        }
    }
}
