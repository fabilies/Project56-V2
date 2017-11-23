using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project56_new.Models
{
    public class FavouritesModel
    {
        public int id { get; set; }
        public int itm_id { get; set; }
        public int user_id { get; set; }
        public DateTime dt_created { get; set; }
    }
}
