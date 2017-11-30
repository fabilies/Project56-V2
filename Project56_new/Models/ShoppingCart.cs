using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project56_new.Models
{
    public class ShoppingCartModel
    {
        public int id { get; set; }
        public int itm_id { get; set; }
        public int ordline_id { get; set; }
        public string description { get; set; }
        public string long_description { get; set; }
        public float price { get; set; }
        public string photo_url { get; set; }
        public int qty { get; set; }

        public float Total { get; set; }
        public float subtotal { get; set; }
        public int stock { get; set; }     
        public int ord_ad { get; set; }
    }
}
