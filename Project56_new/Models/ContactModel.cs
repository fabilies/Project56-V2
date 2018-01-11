using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project56_new.Models
{
    public class ContactModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Naam mag alleen uit leters bestaan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Naam moet tussen de 3 en 30 karakters zijn")]
        public string naam { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Onderwerp moet tussen de 3 en 30 karakters zijn")]
        public string onderwerp { get; set; }
        [Required]
        public string bericht { get; set; }
    }
}
