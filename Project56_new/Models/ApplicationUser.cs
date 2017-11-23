using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project56_new.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "ID")]
        public override string Id { get; set; }
        [Display(Name = "Gebruikersnaam")]
        public override string UserName { get; set; }
        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        public override string PasswordHash { get; set; }
        [Display(Name = "Voornaam")]
        public string firstname { get; set; }
        [Display(Name = "Tv.")]
        public string middlename { get; set; }
        [Display(Name = "Achternaam")]
        public string lastname { get; set; }
        [Display(Name = "Geboortedatum")]
        public DateTime dt_birth { get; set; }
        [Display(Name = "Geslacht")]
        public string gender { get; set; }
        [Display(Name = "Postcode")]
        public string a_zipcode { get; set; }
        [Display(Name = "Adres")]
        public string a_adres { get; set; }
        [Display(Name = "Stad")]
        public string a_city { get; set; }
        [Display(Name = "Huisnummer")]
        public string a_number { get; set; }
        [Display(Name = "Email")]
        public override string Email { get; set; }
        [Display(Name = "Telefoon nummer")]
        public override string PhoneNumber { get; set; }
    }
}
