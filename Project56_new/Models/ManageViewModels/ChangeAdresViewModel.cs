using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project56_new.Models.ManageViewModels
{
    public class ChangeAdresViewModel
    {
        [Required(ErrorMessage ="Invullen verplicht")]
        [Display(Name = "Postcode")]
        public string a_zipcode { get; set; }
        [Required(ErrorMessage = "Invullen verplicht")]
        [Display(Name = "Straatnaam")]
        public string a_adres { get; set; }
        [Required(ErrorMessage = "Invullen verplicht")]
        [Display(Name = "Plaats")]
        public string a_city { get; set; }

        [Required(ErrorMessage ="invullen verplicht")]
        [Display(Name ="Huisnummer (en toevoeging)")]
        public string a_number { get; set; }

        public string StatusMessage { get; set; }
    }
}
