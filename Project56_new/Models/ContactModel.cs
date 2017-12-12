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
        [StringLength(30, MinimumLength = 3)]
        public string naam { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string onderwerp { get; set; }
        [Required]
        public string bericht { get; set; }
    }
}
