using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project56_new.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord bevestigen")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }

        [Display(Name = "Tv.")]
        public string Middlename { get; set; }

        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }

        [Display(Name = "Geboortedatum")]
        public DateTime Dt_birth { get; set; }

        [Display(Name = "Geslacht")]
        public string Gender { get; set; }

        [Display(Name = "Postcode")]
        public string Zipcode { get; set; }

        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }

        [Display(Name = "Huisnummer")]
        public string Homenumber { get; set; }

        [Display(Name = "Telefoon nummer")]
        public string Phonenumber { get; set; }
    }
}
