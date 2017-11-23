using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project56_new.Models
{
    public class WishlistModel
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
    }

    public class WishMains
    {
        public int id { get; set; }

        public string user_ad { get; set; }
        public int ordstatus_id { get; set; }
        public DateTime dt_order { get; set; }
        public DateTime dt_delivery { get; set; }

        public int l_show { get; set; }
        public DateTime dt_created { get; set; }
        public DateTime dt_modified { get; set; }
    }

    public class WishLines
    {
        public int id { get; set; }
        public int itm_id { get; set; }
        public int ord_id { get; set; }
        public int qty { get; set; }
        public int l_show { get; set; }
        public DateTime dt_created { get; set; }
        public DateTime dt_modified { get; set; }
    }

    public class WishStatus
    {
        public int id { get; set; }
        public string description { get; set; }
        public int l_show { get; set; }
        public DateTime dt_created { get; set; }
        public DateTime dt_modified { get; set; }
    }

    public class WishHistory
    {
        public int id { get; set; }
        public string description { get; set; }
        public int l_show { get; set; }
        public DateTime dt_created { get; set; }
        public DateTime dt_modified { get; set; }
    }

}
