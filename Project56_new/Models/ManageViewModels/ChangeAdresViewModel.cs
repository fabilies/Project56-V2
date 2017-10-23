using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project56_new.Models.ManageViewModels
{
    public class ChangeAdresViewModel
    {
        [Display(Name = "Postcode")]
        public string a_zipcode { get; set; }
        [Display(Name = "Adres")]
        public string a_adres { get; set; }
        [Display(Name = "Plaats")]
        public string a_city { get; set; }

        public string StatusMessage { get; set; }
    }
}
