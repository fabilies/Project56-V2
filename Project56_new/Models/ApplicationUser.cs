using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Project56_new.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string a_zipcode { get; set; }

        public string a_adres { get; set; }
        public string a_city { get; set; }
        public string a_number { get; set; }

        public string firstname { get; set; }

        public string middlename { get; set; }

        public string lastname { get; set; }

        public DateTime dt_birth { get; set; }
        public string gender { get; set; }
    }
}
