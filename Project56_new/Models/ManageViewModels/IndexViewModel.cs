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

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefoonnummer")]

        public string PhoneNumber { get; set; }

        //[Required]
        //public string Fullname { get; set; }


        //[Required]

        //public DateTime Dt_Birth { get; set; }
       
        //public string Gender { get; set; }


        public string StatusMessage { get; set; }


    }
}
