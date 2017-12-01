using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Project56_new.Models

{
    public  class OrdStatus {
        public int id {get;set;}     
        public string description{get;set;}
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}   
    }
    public  class OrdMains {
        public int id {get;set;}         
        public string user_ad {get;set;}
        public int ordstatus_id {get;set;}
        public DateTime dt_order {get;set;}
        public DateTime dt_delivery{get;set;}
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}   
    }
    public  class OrdHistory {
        public int id {get;set;}
        [Display(Name="Artikel")]
        public string itm_description { get; set; }
        [Display(Name = "Aantal besteld")]
        public int qty_bought { get; set; }
        public string user_ad { get; set; }
        [Display(Name = "Order Nr")]
        public int ord_id { get; set; }
        public int ordline_id {get;set;}
        [Display(Name = "Prijs")]
        public float priced_payed { get; set; }
        [Display(Name = "Datum aangemaakt")]
        public DateTime dt_created {get;set;} 
    }
    public  class OrdLines {
        public int id {get;set;}           
        public int itm_id {get;set;}
        public int ord_id {get;set;}
        public int qty {get;set;}
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}   
    }
}