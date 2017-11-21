using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project56_new.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Gebruikersnaam")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required(ErrorMessage ="Email invullen is verplicht")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefoonnummer invullen is verplicht")]
        [Phone]
        [Display(Name = "Telefoonnummer")]

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Voornaam invullen is verplicht")]
        [Display(Name = "Voornaam")]
        public string firstname { get; set; }

        [Display(Name = "Tussenvoegsel")]
        public string middlename { get; set; }

        [Required(ErrorMessage = "Achternaam invullen is verplicht")]
        [Display(Name = "Achternaam")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Geboortedatum invullen is verplicht")]
        [Display(Name = "Geboortedatum")]
        public DateTime dt_birth { get; set; }

        [Required]
        public string gender { get; set; }


        public string StatusMessage { get; set; }


    }
}
